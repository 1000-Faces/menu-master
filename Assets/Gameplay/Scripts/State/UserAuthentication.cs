using Firebase.Auth;
using Firebase;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class User
{
    public string uid;

    public string username;

    public string email;

    public string password;

    public Uri photoUrl;
}

public class UserAuthentication : MonoBehaviour
{
    public static UserAuthentication Instance { get; private set; }

    FirebaseAuth auth;
    FirebaseUser user;

    // Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;

    public event EventHandler UserChanged;

    public bool IsLoggedIn => auth.CurrentUser != null;

    void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();

                // Set a flag here to indicate whether Firebase is ready to use by your app.
                Debug.Log("Firebase initialized");
            }
            else
            {
                Debug.LogError($"Firebase Auth: Could not resolve all Firebase dependencies: {dependencyStatus}");
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }

    void OnDestroy()
    {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }

    void InitializeFirebase()
    {
        // Create and hold a reference to your FirebaseAuth,
        // where auth is a Firebase.FirebaseAuth property of your application class.
        auth = FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    void AuthStateChanged(object sender, EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null
                && auth.CurrentUser.IsValid();
            if (!signedIn && user != null)
            {
                Debug.Log("Firebase Auth: Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Firebase Auth: Signed in " + user.UserId);
                //displayName = user.DisplayName ?? "";
                //emailAddress = user.Email ?? "";
                //photoUrl = user.PhotoUrl ?? "";

                UserChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void CreateAccount(string email, string password)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("Firebase Auth: CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("Firebase Auth: CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            AuthResult result = task.Result;
            Debug.LogFormat("Firebase Auth: Firebase user created successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);
        });
    }

    public void SignIn(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("Firebase Auth: SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("Firebase Auth: SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            AuthResult result = task.Result;
            Debug.LogFormat("Firebase Auth: User signed in successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);
        });
    }

    public void SignOut()
    {
        auth.SignOut();
    }

    public User GetUser()
    {
        FirebaseUser user = auth.CurrentUser;
        if (user != null)
        {
            User userObj = new()
            {
                // The user's Id, unique to the Firebase project.
                // Do NOT use this value to authenticate with your backend server, if you
                // have one; use User.TokenAsync() instead.
                uid = user.UserId,
                username = user.DisplayName,
                email = user.Email,
                photoUrl = user.PhotoUrl
            };

            return userObj;
        }

        return null;
    }
}
