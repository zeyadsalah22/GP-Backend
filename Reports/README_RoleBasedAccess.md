# Role-Based Access Control (RBAC) Implementation

This document describes the role-based access control system implemented in the GPBackend project.

## Overview

The RBAC system provides secure access control with two distinct roles:
- **User** (Default role for new registrations)
- **Admin** (Elevated privileges for system administration)

## Architecture

### Components

1. **UserRole Enum** - Defines available roles
2. **Authorization Policies** - Configured in Program.cs
3. **JWT Claims** - Role information embedded in tokens
4. **Controller Authorization** - Endpoint-level access control
5. **Role Management** - Admin-only user role modification

## Roles and Permissions

### **User Role (Default)**
- Access to personal data endpoints (`/me`, profile management)
- Can view company listings (read-only)
- Can create and manage their own applications
- Can participate in interviews
- Can manage personal todo lists and resumes

### **Admin Role**
- **Full User Management**: 
  - View all users (`GET /api/users`)
  - View specific user details (`GET /api/users/{id}`)
  - Change user roles (`PUT /api/users/{id}/change-role`)
  - Delete users (`DELETE /api/users/{id}`)
- **Company Management**:
  - Create companies (`POST /api/companies`)
  - Update companies (`PUT /api/companies/{id}`)
  - Delete companies (`DELETE /api/companies/{id}`)
- **All User permissions** (Admin inherits User capabilities)

## API Endpoints Authorization

### **Public Endpoints (No Authentication Required)**
- `POST /api/auth/login`
- `POST /api/auth/register`
- `POST /api/auth/refresh`
- `GET /api/companies` (Company listings)

### **User Endpoints (Self-Management + General Access)**
- `GET /api/users/me`
- `PUT /api/users/{id}` (Own profile or admin can update any)
- `PUT /api/users/change-password`
- `DELETE /api/users/{id}` (Own account or admin can delete any)
- All application, interview, resume, and todo endpoints

### **Admin-Only Endpoints**
- `GET /api/users` (All users)
- `GET /api/users/{id}` (Any user details)
- `PUT /api/users/{id}/change-role`
- `POST /api/companies`
- `PUT /api/companies/{id}`
- `DELETE /api/companies/{id}`

### **Mixed Access Endpoints (User for own data, Admin for any data)**
- `PUT /api/users/{id}` - Users can update their own profile, Admins can update any user's profile
- `DELETE /api/users/{id}` - Users can delete their own account, Admins can delete any user account

## Implementation Details

### 1. **Database Schema**
```sql
-- Users table includes role column
ALTER TABLE Users ADD role int NOT NULL DEFAULT 0;
-- 0 = User, 1 = Admin
```

### 2. **JWT Token Claims**
```csharp
var claims = new[]
{
    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
    new Claim(ClaimTypes.Name, $"{user.Fname} {user.Lname}"),
    new Claim(ClaimTypes.Email, user.Email),
    new Claim(ClaimTypes.Role, user.Role.ToString()) // Role claim
};
```

### 3. **Authorization Policies**
```csharp
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => 
        policy.RequireRole("Admin"));
    
    options.AddPolicy("UserOrAdmin", policy => 
        policy.RequireRole("User", "Admin"));
});
```

### 4. **Controller Authorization Examples**
```csharp
[Authorize(Policy = "AdminOnly")]
public async Task<IActionResult> GetAllUsers() { }

[Authorize(Policy = "UserOrAdmin")]
public async Task<IActionResult> GetCurrentUser() { }

[AllowAnonymous]
public async Task<IActionResult> GetAllCompanies() { }
```

## Role Management

### **Changing User Roles (Admin Only)**

**Endpoint**: `PUT /api/users/{id}/change-role`

**Request Body**:
```json
{
  "userId": 123,
  "role": 1  // 0 = User, 1 = Admin
}
```

**Response**:
```json
{
  "message": "User role updated successfully"
}
```

## Security Features

### **Token-Based Authentication**
- JWT tokens include role claims
- Tokens expire after 15 minutes (configurable)
- Refresh tokens for seamless user experience

### **Authorization Middleware**
- Automatic role validation on protected endpoints
- Policy-based authorization for fine-grained control
- Blacklist support for revoked tokens

### **Default Security**
- New users default to "User" role
- Admin role must be explicitly assigned
- Role changes require admin privileges

## Usage Examples

### **1. Admin Creating Another Admin**
```bash
# Admin login
POST /api/auth/login
{
  "email": "admin@example.com",
  "password": "password"
}

# Change user role to Admin
PUT /api/users/123/change-role
Authorization: Bearer {admin_token}
{
  "userId": 123,
  "role": 1
}
```

### **2. User Accessing Protected Resource**
```bash
# User login
POST /api/auth/login
{
  "email": "user@example.com", 
  "password": "password"
}

# Access personal profile
GET /api/users/me
Authorization: Bearer {user_token}
```

### **3. Role-Based Response Differences**
```bash
# Admin can see all users
GET /api/users
Authorization: Bearer {admin_token}
# Returns: Array of all users

# User cannot access this endpoint
GET /api/users  
Authorization: Bearer {user_token}
# Returns: 403 Forbidden
```

## Error Responses

### **Unauthorized (401)**
```json
{
  "message": "Unauthorized"
}
```

### **Forbidden (403)**
```json
{
  "message": "Access denied. Insufficient privileges."
}
```

### **Role Change Errors**
```json
{
  "message": "User not found"
}
```

## Migration and Deployment

### **Database Migration**
```bash
dotnet ef migrations add AddUserRoleColumn
dotnet ef database update
```

### **Existing Users**
- Existing users will default to "User" role (0)
- First admin must be created manually in database or through seeding

## Best Practices

1. **Principle of Least Privilege**: Users start with minimal permissions
2. **Explicit Admin Assignment**: Admin roles must be explicitly granted
3. **Token Validation**: Always validate role claims on sensitive operations
4. **Audit Trail**: Consider logging role changes for security auditing
5. **Role Separation**: Keep admin and user functionalities clearly separated

## Testing

### **Test Scenarios**
1. User registration defaults to User role
2. User cannot access admin endpoints
3. Admin can access all endpoints
4. Role changes require admin privileges
5. JWT tokens include correct role claims
6. Authorization policies work correctly

### **Test Users**
Create test users for each role:
```sql
-- Test Admin User
INSERT INTO Users (fname, lname, email, password, role) 
VALUES ('Admin', 'User', 'admin@test.com', 'hashed_password', 1);

-- Test Regular User  
INSERT INTO Users (fname, lname, email, password, role)
VALUES ('Regular', 'User', 'user@test.com', 'hashed_password', 0);
```

This RBAC implementation provides a secure, scalable foundation for managing user permissions in the GPBackend application. 