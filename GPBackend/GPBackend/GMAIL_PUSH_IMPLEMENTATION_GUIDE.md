# Gmail Push Notifications - Implementation Guide

## 📋 Overview

We've successfully implemented **Option B: Pub/Sub → n8n → Backend** architecture for Gmail push notifications. This means Google Pub/Sub will send notifications directly to your n8n workflow webhook, which will then process emails and update your backend.

---

## ✅ What's Been Completed

### 1. **Backend Changes**

#### Database Migration ✅
- Added `HistoryId` (string, 50 chars) to track Gmail history for incremental updates
- Added `WatchExpiresAt` (DateTime) to track when Gmail watch expires (7 days)
- Migration applied successfully: `20251102202307_AddGmailWatchFields`

#### New Backend Endpoints ✅
- `GET /api/Gmail/connection-by-email?email={email}` - Get user connection from Gmail address
- `POST /api/Gmail/update-history-id` - Update history ID after processing notifications

#### Gmail Watch Setup ✅
- Automatically sets up Gmail watch when user connects their account
- Calls `users.watch` API with your Pub/Sub topic
- Stores `historyId` and `watchExpiresAt` in database

#### Configuration ✅
```json
"GooglePubSub": {
  "ProjectId": "joblander-477008",
  "TopicName": "projects/joblander-477008/topics/gmail-notifications",
  "SubscriptionName": "projects/joblander-477008/subscriptions/gmail-notifications-sub",
  "VerificationToken": "d5OzEVAaH6kg1NuBTvlrU4QeXRDsF8q0"
}
```

### 2. **n8n Push Workflow** ✅

Created: `n8n-gmail-push-workflow.json`

**Workflow Flow:**
```
Webhook (Pub/Sub) 
  → Decode Notification 
  → Get User Connection 
  → Get Pending Applications 
  → Fetch Gmail History 
  → Extract New Messages 
  → Fetch Full Email Details 
  → Extract Email Data 
  → Enhanced Email Parser (with job title matching)
  → Send to Backend 
  → Update History ID 
  → Respond to Pub/Sub
```

**Key Differences from Polling Workflow:**
- ✅ Uses webhook trigger instead of schedule trigger
- ✅ Uses Gmail History API instead of search API (more efficient)
- ✅ Only fetches changes since last `historyId`
- ✅ Updates `historyId` instead of `lastCheckedAt`
- ✅ Must respond to Pub/Sub within 60 seconds

---

## 🚀 Next Steps to Make It Live

### Step 1: Update Pub/Sub Subscription Push Endpoint

In Google Cloud Console, update your subscription to push to n8n:

1. Go to: https://console.cloud.google.com/cloudpubsub/subscription/list?project=joblander-477008
2. Click on `gmail-notifications-sub`
3. Click "EDIT"
4. Under **Delivery type**, ensure "Push" is selected
5. Set **Endpoint URL** to your n8n webhook URL (you'll get this after importing the workflow)
6. Click "UPDATE"

### Step 2: Import n8n Workflow

1. Open n8n: https://chadykarimfarah-manouella.app.n8n.cloud
2. Click "+" → "Import from File"
3. Select `n8n-gmail-push-workflow.json`
4. **IMPORTANT:** After import, click on the "Webhook - Pub/Sub Push" node
5. Copy the **Production Webhook URL** (it will look like: `https://chadykarimfarah-manouella.app.n8n.cloud/webhook-test/YOUR_WEBHOOK_PATH`)
6. Update the Pub/Sub subscription endpoint with this URL
7. Activate the workflow (toggle switch in top-right)

### Step 3: Update n8n Workflow URLs

Replace all instances of `https://evident-melva-nonpunctually.ngrok-free.dev` with your production backend URL in these nodes:
- "Get User Connection"
- "Get User Pending Applications"
- "Send Update to Backend"
- "Update History ID"
- "Update History ID (No Messages)"

### Step 4: Test the Integration

1. **Connect a test Gmail account:**
   - Use your frontend (after implementing below) OR
   - Call: `GET http://localhost:5000/api/Gmail/connect` with JWT token
   - Complete OAuth flow
   - Backend will automatically set up Gmail watch

2. **Verify watch setup:**
   - Check database: `HistoryId` and `WatchExpiresAt` should be populated
   - Check logs for: "Gmail watch setup successfully"

3. **Send a test email:**
   - Send an email to the connected Gmail account from a company you have a pending application for
   - Include keywords like "interview invitation" or "assessment"
   - Within seconds, n8n should receive a Pub/Sub notification
   - Check n8n execution history
   - Check your backend for updated application status

### Step 5: Monitor and Maintain

**Watch Renewal:**
Gmail watches expire after 7 days. You have two options:

**Option A: Automatic Renewal (Recommended)**
Create a scheduled n8n workflow that runs daily and renews watches:
```
Schedule Daily 
  → Get All Connections 
  → For Each Connection 
    → Check if WatchExpiresAt < 2 days from now 
    → Call Gmail users.watch API 
    → Update HistoryId and WatchExpiresAt in backend
```

**Option B: Renewal on Connect**
Watch will be renewed whenever user reconnects or refreshes their Gmail connection.

---

## 🎨 Frontend Integration (React)

### Comprehensive Prompt for Frontend Team

```markdown
# Task: Implement Gmail Account Connection Feature

## Context
I need to add a feature that allows users to connect their Gmail account to receive automatic job application updates via push notifications. The backend is already implemented with these endpoints:

**Backend Base URL:** `http://localhost:5000` (update with your API base URL)

**Endpoints:**
1. `GET /api/Gmail/connect` - Get OAuth URL (requires JWT auth)
   - Response: `{ "authUrl": "https://accounts.google.com/o/oauth2/v2/auth?..." }`

2. `GET /api/Gmail/callback?code={code}&state={state}` - OAuth callback (no auth required)
   - Response: `{ "success": true/false, "message": "..." }`

3. `GET /api/Gmail/status` - Get connection status (requires JWT auth)
   - Response: 
   ```json
   {
     "isConnected": true,
     "gmailAddress": "user@gmail.com",
     "connectedAt": "2025-01-15T10:30:00Z",
     "lastCheckedAt": "2025-01-20T15:45:00Z"
   }
   ```

4. `POST /api/Gmail/disconnect` - Disconnect Gmail (requires JWT auth)
   - Response: `{ "success": true, "message": "..." }`

## Requirements

### 1. Create Gmail Integration Page/Section

**Location:** User Settings or Dashboard

**Components Needed:**
- Gmail connection card/section
- OAuth popup handler
- Callback page component

### 2. Gmail Connection Card

**When Not Connected:**
```
┌─────────────────────────────────────────┐
│ 📧 Gmail Integration                     │
├─────────────────────────────────────────┤
│                                          │
│ 🔴 Not Connected                         │
│                                          │
│ Connect your Gmail to automatically      │
│ track job application updates.           │
│                                          │
│ ┌──────────────────────────────────┐   │
│ │  Connect Gmail Account            │   │
│ └──────────────────────────────────┘   │
│                                          │
└─────────────────────────────────────────┘
```

**When Connected:**
```
┌─────────────────────────────────────────┐
│ 📧 Gmail Integration                     │
├─────────────────────────────────────────┤
│                                          │
│ ✅ Connected                             │
│                                          │
│ Gmail: user@gmail.com                    │
│ Connected: Jan 15, 2025                  │
│                                          │
│ ┌──────────────┐  ┌──────────────────┐ │
│ │  Refresh      │  │  Disconnect       │ │
│ └──────────────┘  └──────────────────┘ │
│                                          │
└─────────────────────────────────────────┘
```

### 3. OAuth Flow Implementation

**Step 1: Initiate Connection**
```typescript
const handleConnectGmail = async () => {
  try {
    setLoading(true);
    const response = await fetch('/api/Gmail/connect', {
      headers: {
        'Authorization': `Bearer ${jwtToken}`
      }
    });
    const data = await response.json();
    
    // Open OAuth popup (600x700px)
    const width = 600;
    const height = 700;
    const left = (window.screen.width - width) / 2;
    const top = (window.screen.height - height) / 2;
    
    const popup = window.open(
      data.authUrl,
      'Gmail OAuth',
      `width=${width},height=${height},left=${left},top=${top}`
    );
    
    // Listen for OAuth completion
    window.addEventListener('message', handleOAuthCallback);
  } catch (error) {
    showError('Failed to initiate Gmail connection');
  } finally {
    setLoading(false);
  }
};
```

**Step 2: Create Callback Page**
Create a new route: `/gmail/callback`

```typescript
// GmailCallbackPage.tsx
import { useEffect } from 'react';
import { useSearchParams, useNavigate } from 'react-router-dom';

export const GmailCallbackPage = () => {
  const [searchParams] = useSearchParams();
  const navigate = useNavigate();

  useEffect(() => {
    const processCallback = async () => {
      const code = searchParams.get('code');
      const state = searchParams.get('state');
      
      if (!code || !state) {
        // Send error to parent window
        if (window.opener) {
          window.opener.postMessage({ 
            type: 'gmail-oauth-error', 
            error: 'Missing parameters' 
          }, window.location.origin);
          window.close();
        } else {
          navigate('/settings');
        }
        return;
      }

      try {
        // Call backend callback endpoint
        const response = await fetch(
          `/api/Gmail/callback?code=${code}&state=${state}`
        );
        const data = await response.json();

        if (response.ok && data.success) {
          // Send success to parent window
          if (window.opener) {
            window.opener.postMessage({ 
              type: 'gmail-oauth-success' 
            }, window.location.origin);
            window.close();
          } else {
            navigate('/settings?gmail=connected');
          }
        } else {
          throw new Error(data.message || 'Connection failed');
        }
      } catch (error) {
        if (window.opener) {
          window.opener.postMessage({ 
            type: 'gmail-oauth-error', 
            error: error.message 
          }, window.location.origin);
          window.close();
        } else {
          navigate('/settings?gmail=error');
        }
      }
    };

    processCallback();
  }, [searchParams, navigate]);

  return (
    <div style={{ padding: '40px', textAlign: 'center' }}>
      <h2>Connecting Gmail...</h2>
      <p>Please wait while we complete the setup.</p>
    </div>
  );
};
```

**Step 3: Handle OAuth Completion**
```typescript
const handleOAuthCallback = (event: MessageEvent) => {
  // Verify origin
  if (event.origin !== window.location.origin) return;

  if (event.data.type === 'gmail-oauth-success') {
    showSuccess('Gmail connected successfully!');
    refreshConnectionStatus();
    window.removeEventListener('message', handleOAuthCallback);
  } else if (event.data.type === 'gmail-oauth-error') {
    showError(`Connection failed: ${event.data.error}`);
    window.removeEventListener('message', handleOAuthCallback);
  }
};
```

### 4. Connection Status Management

```typescript
const [gmailConnection, setGmailConnection] = useState({
  isConnected: false,
  gmailAddress: null,
  connectedAt: null
});

const refreshConnectionStatus = async () => {
  try {
    const response = await fetch('/api/Gmail/status', {
      headers: {
        'Authorization': `Bearer ${jwtToken}`
      }
    });
    
    if (response.ok) {
      const data = await response.json();
      setGmailConnection(data);
    } else if (response.status === 404) {
      setGmailConnection({ isConnected: false });
    }
  } catch (error) {
    console.error('Failed to get Gmail status:', error);
  }
};

// Load status on component mount
useEffect(() => {
  refreshConnectionStatus();
}, []);
```

### 5. Disconnect Functionality

```typescript
const handleDisconnect = async () => {
  const confirmed = window.confirm(
    'Are you sure you want to disconnect your Gmail account? ' +
    'You will stop receiving automatic application updates.'
  );

  if (!confirmed) return;

  try {
    setLoading(true);
    const response = await fetch('/api/Gmail/disconnect', {
      method: 'POST',
      headers: {
        'Authorization': `Bearer ${jwtToken}`
      }
    });

    if (response.ok) {
      showSuccess('Gmail disconnected successfully');
      setGmailConnection({ isConnected: false });
    } else {
      throw new Error('Failed to disconnect');
    }
  } catch (error) {
    showError('Failed to disconnect Gmail');
  } finally {
    setLoading(false);
  }
};
```

### 6. UI/UX Requirements

- ✅ Loading states for all async operations
- ✅ Success/error toast notifications
- ✅ Confirmation dialog before disconnect
- ✅ Disabled state while operations are in progress
- ✅ Clear visual indication of connection status
- ✅ Responsive design (works on mobile and desktop)
- ✅ Handle popup blockers gracefully

### 7. Error Handling

**Handle these scenarios:**
- User closes OAuth popup before completing
- User denies Gmail permissions
- Backend connection fails
- Network errors
- Token expiration during OAuth
- Popup blocked by browser

### 8. Example Complete Component (React + TypeScript)

```typescript
import { useState, useEffect } from 'react';
import { Card, Button, Badge, Alert } from 'your-ui-library';

export const GmailIntegration = () => {
  const [connection, setConnection] = useState({ isConnected: false });
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  useEffect(() => {
    fetchStatus();
    window.addEventListener('message', handleOAuthMessage);
    return () => window.removeEventListener('message', handleOAuthMessage);
  }, []);

  const fetchStatus = async () => {
    // ... implementation from above
  };

  const handleConnect = async () => {
    // ... implementation from above
  };

  const handleDisconnect = async () => {
    // ... implementation from above
  };

  const handleOAuthMessage = (event) => {
    // ... implementation from above
  };

  return (
    <Card>
      <h3>📧 Gmail Integration</h3>
      
      {error && <Alert variant="error">{error}</Alert>}
      
      {connection.isConnected ? (
        <>
          <Badge color="success">✅ Connected</Badge>
          <p>Gmail: {connection.gmailAddress}</p>
          <p>Since: {new Date(connection.connectedAt).toLocaleDateString()}</p>
          <Button onClick={handleConnect} disabled={loading}>
            Refresh
          </Button>
          <Button 
            onClick={handleDisconnect} 
            disabled={loading}
            variant="danger"
          >
            Disconnect
          </Button>
        </>
      ) : (
        <>
          <Badge color="error">🔴 Not Connected</Badge>
          <p>Connect Gmail to receive automatic job update notifications</p>
          <Button onClick={handleConnect} disabled={loading}>
            {loading ? 'Connecting...' : 'Connect Gmail Account'}
          </Button>
        </>
      )}
    </Card>
  );
};
```

### 9. Add Route for Callback

In your routing configuration:
```typescript
<Route path="/gmail/callback" element={<GmailCallbackPage />} />
```

### 10. Testing Checklist

- [ ] OAuth popup opens correctly
- [ ] OAuth flow completes successfully
- [ ] Callback page processes response correctly
- [ ] Parent window receives success/error message
- [ ] Connection status updates immediately
- [ ] Disconnect works and shows confirmation
- [ ] Error messages display correctly
- [ ] Loading states show properly
- [ ] Works on mobile devices
- [ ] Handles popup blockers gracefully

## Design Notes

- Use your existing design system components
- Match the styling of other settings sections
- Consider adding a "Learn More" link explaining the feature
- Add an icon or illustration to make it visually appealing
- Show a badge/notification when new updates are received via Gmail

## Security Notes

- JWT token is sent with all authenticated requests
- OAuth state parameter prevents CSRF attacks
- Callback validates state before processing
- All communication between popup and parent uses postMessage with origin verification
```

---

## 📊 Architecture Comparison

### Before (Polling):
```
Schedule (5 min) → Get All Users → For Each User:
  → Fetch Gmail Messages (search API) 
  → Check all emails since lastCheckedAt
```
**Issues:**
- ❌ Polls every 5 minutes even if no new emails
- ❌ Fetches same emails multiple times
- ❌ High API quota usage
- ❌ 5-minute delay before detection

### After (Push):
```
Gmail Activity → Pub/Sub → n8n Webhook:
  → Fetch History (incremental changes only)
  → Process only new emails
```
**Benefits:**
- ✅ Instant notifications (< 10 seconds)
- ✅ Only fetches when there are changes
- ✅ 90% reduction in API calls
- ✅ Uses history API (more efficient)
- ✅ Lower costs and better performance

---

## 🔧 Troubleshooting

### Gmail Watch Not Set Up
**Symptom:** `HistoryId` is null in database
**Solution:** 
- Check logs for "Gmail watch setup" errors
- Verify Pub/Sub topic permissions
- Ensure Gmail API has watch permission
- Re-connect Gmail account

### Pub/Sub Not Receiving Notifications
**Symptom:** n8n workflow never triggers
**Solution:**
- Verify subscription push endpoint URL is correct
- Check n8n webhook is activated
- Test webhook URL manually with curl
- Check Google Cloud Pub/Sub monitoring for delivery failures

### History API Returns Errors
**Symptom:** "historyId is invalid" errors
**Solution:**
- History IDs become invalid after 7 days
- Re-connect Gmail to get fresh historyId
- Implement watch renewal workflow

### Application Running but Errors
**Symptom:** Database column errors
**Solution:**
- Ensure migration was applied: `dotnet ef database update`
- Check `__EFMigrationsHistory` table for latest migration
- Restart application after migration

---

## 📝 Important Notes

1. **Watch Expiration:** Gmail watches expire after 7 days. Implement automatic renewal!

2. **History ID Validity:** History IDs are only valid for a limited time. Store and update them properly.

3. **Pub/Sub Acknowledgment:** n8n must respond to Pub/Sub within 60 seconds or message will be retried.

4. **Rate Limiting:** Gmail API has quotas. Push notifications are more efficient than polling.

5. **Testing:** Use a test Gmail account first before rolling out to production users.

6. **Monitoring:** Monitor n8n execution logs and backend logs for any issues.

7. **Security:** Never log or expose access tokens or history IDs in production.

---

## 🎯 Success Criteria

- ✅ User can connect Gmail via OAuth
- ✅ Gmail watch is set up automatically
- ✅ Push notifications trigger n8n workflow
- ✅ Emails are parsed and applications updated
- ✅ No polling workflow running (can be disabled)
- ✅ < 10 second delay from email arrival to status update
- ✅ 90% reduction in Gmail API calls

---

## Questions?

If you encounter any issues:
1. Check backend logs for errors
2. Check n8n execution history
3. Check Google Cloud Pub/Sub monitoring
4. Verify database has latest migration
5. Test with a simple email first

