# JWT Refresh Token Implementation

This document describes the JWT refresh token implementation in the GPBackend project.

## Overview

The refresh token system provides a secure way to maintain user sessions without requiring frequent re-authentication. It implements the following security features:

- **Short-lived access tokens** (15 minutes)
- **Long-lived refresh tokens** (7 days)
- **Token rotation** (new refresh token issued on each refresh)
- **Automatic cleanup** of expired tokens
- **Token revocation** support

## Architecture

### Components

1. **RefreshToken Entity** - Database model for storing refresh tokens
2. **RefreshTokenRepository** - Data access layer for refresh token operations
3. **RefreshTokenService** - Business logic for token management
4. **JwtService** - Enhanced with refresh token generation
5. **AuthController** - Updated with refresh token endpoints
6. **TokenCleanupService** - Background service for cleanup

### Database Schema

```sql
CREATE TABLE RefreshTokens (
    refresh_token_id int IDENTITY(1,1) PRIMARY KEY,
    token nvarchar(500) NOT NULL,
    user_id int NOT NULL,
    expiry_date datetime2(0) NOT NULL,
    is_revoked bit NOT NULL DEFAULT 0,
    is_used bit NOT NULL DEFAULT 0,
    created_at datetime2(0) NOT NULL DEFAULT (sysutcdatetime()),
    replaced_by_token nvarchar(500) NULL,
    FOREIGN KEY (user_id) REFERENCES Users(user_id)
);
```

## API Endpoints

### 1. Login
**POST** `/api/auth/login`

**Request:**
```json
{
    "email": "user@example.com",
    "password": "password123"
}
```

**Response:**
```json
{
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "refreshToken": "base64-encoded-refresh-token",
    "userId": 1,
    "email": "user@example.com",
    "fullName": "John Doe",
    "expiresAt": "2024-01-01T12:15:00Z"
}
```

### 2. Register
**POST** `/api/auth/register`

**Request:**
```json
{
    "fname": "John",
    "lname": "Doe",
    "email": "user@example.com",
    "password": "password123"
}
```

**Response:** Same as login

### 3. Refresh Token
**POST** `/api/auth/refresh`

**Request:**
```json
{
    "refreshToken": "base64-encoded-refresh-token"
}
```

**Response:**
```json
{
    "token": "new-jwt-token",
    "refreshToken": "new-refresh-token",
    "userId": 1,
    "email": "user@example.com",
    "fullName": "John Doe",
    "expiresAt": "2024-01-01T12:15:00Z"
}
```

### 4. Revoke Token
**POST** `/api/auth/revoke`

**Headers:** `Authorization: Bearer <jwt-token>`

**Request:**
```json
{
    "refreshToken": "base64-encoded-refresh-token"
}
```

**Response:**
```json
{
    "message": "Token revoked successfully"
}
```

### 5. Logout
**POST** `/api/auth/logout`

**Headers:** `Authorization: Bearer <jwt-token>`

**Request (Optional):**
```json
{
    "refreshToken": "base64-encoded-refresh-token"
}
```

**Response:**
```json
{
    "message": "Successfully logged out"
}
```

## Configuration

### appsettings.json
```json
{
    "JwtSettings": {
        "SecretKey": "your-secret-key",
        "Issuer": "GPBackendAPI",
        "Audience": "GPFrontend",
        "ExpiryInMinutes": 15,
        "RefreshTokenExpiryInDays": 7
    }
}
```

## Security Features

### 1. Token Rotation
- Each refresh operation generates a new refresh token
- Old refresh token is marked as used
- Prevents token replay attacks

### 2. Token Validation
- Refresh tokens are validated for:
  - Existence in database
  - Expiration status
  - Revocation status
  - Usage status

### 3. Automatic Cleanup
- Background service runs daily
- Removes expired refresh tokens
- Keeps database clean

### 4. Token Blacklisting
- Access tokens are blacklisted on logout
- Prevents use of compromised tokens

## Usage Examples

### Frontend Implementation (JavaScript)

```javascript
class AuthService {
    constructor() {
        this.accessToken = localStorage.getItem('accessToken');
        this.refreshToken = localStorage.getItem('refreshToken');
    }

    async login(email, password) {
        const response = await fetch('/api/auth/login', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ email, password })
        });

        if (response.ok) {
            const data = await response.json();
            this.setTokens(data.token, data.refreshToken);
            return data;
        }
        throw new Error('Login failed');
    }

    async refreshAccessToken() {
        if (!this.refreshToken) {
            throw new Error('No refresh token available');
        }

        const response = await fetch('/api/auth/refresh', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ refreshToken: this.refreshToken })
        });

        if (response.ok) {
            const data = await response.json();
            this.setTokens(data.token, data.refreshToken);
            return data.token;
        }
        
        // Refresh failed, redirect to login
        this.logout();
        throw new Error('Token refresh failed');
    }

    async apiCall(url, options = {}) {
        let token = this.accessToken;
        
        // Try the request with current token
        let response = await fetch(url, {
            ...options,
            headers: {
                ...options.headers,
                'Authorization': `Bearer ${token}`
            }
        });

        // If unauthorized, try to refresh token
        if (response.status === 401) {
            try {
                token = await this.refreshAccessToken();
                response = await fetch(url, {
                    ...options,
                    headers: {
                        ...options.headers,
                        'Authorization': `Bearer ${token}`
                    }
                });
            } catch (error) {
                // Refresh failed, user needs to login again
                window.location.href = '/login';
                return;
            }
        }

        return response;
    }

    setTokens(accessToken, refreshToken) {
        this.accessToken = accessToken;
        this.refreshToken = refreshToken;
        localStorage.setItem('accessToken', accessToken);
        localStorage.setItem('refreshToken', refreshToken);
    }

    async logout() {
        if (this.refreshToken) {
            await fetch('/api/auth/logout', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${this.accessToken}`
                },
                body: JSON.stringify({ refreshToken: this.refreshToken })
            });
        }

        this.accessToken = null;
        this.refreshToken = null;
        localStorage.removeItem('accessToken');
        localStorage.removeItem('refreshToken');
    }
}
```

## Best Practices

### 1. Token Storage
- Store refresh tokens securely (HttpOnly cookies recommended for web)
- Never expose refresh tokens in URLs or logs
- Use secure storage on mobile devices

### 2. Error Handling
- Handle token expiration gracefully
- Implement automatic token refresh
- Redirect to login on refresh failure

### 3. Security Considerations
- Use HTTPS in production
- Implement rate limiting on auth endpoints
- Monitor for suspicious token usage patterns
- Consider implementing device tracking

### 4. Token Lifecycle
- Set appropriate expiration times
- Implement token rotation
- Provide manual token revocation
- Clean up expired tokens regularly

## Troubleshooting

### Common Issues

1. **"Invalid or expired refresh token"**
   - Check if refresh token exists in database
   - Verify token hasn't expired
   - Ensure token hasn't been revoked or used

2. **"Token refresh failed"**
   - Verify database connection
   - Check JWT configuration
   - Ensure user still exists

3. **Performance Issues**
   - Monitor database query performance
   - Consider indexing refresh token table
   - Implement token cleanup scheduling

### Monitoring

Monitor the following metrics:
- Token refresh frequency
- Failed refresh attempts
- Token cleanup statistics
- Database performance

## Migration Notes

If upgrading from a system without refresh tokens:
1. Run the database migration
2. Update client applications to handle refresh tokens
3. Consider implementing a grace period for old tokens
4. Monitor for any authentication issues

## Security Audit Checklist

- [ ] Refresh tokens are stored securely
- [ ] Token rotation is implemented
- [ ] Expired tokens are cleaned up
- [ ] Rate limiting is in place
- [ ] HTTPS is enforced
- [ ] Logging is implemented for security events
- [ ] Token validation is comprehensive
- [ ] Error messages don't leak sensitive information 