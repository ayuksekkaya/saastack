﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Application.Interfaces {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Audits {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Audits() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Application.Interfaces.Audits", typeof(Audits).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to EndUser.PlatformRolesAssigned.
        /// </summary>
        public static string EndUserApplication_PlatformRolesAssigned {
            get {
                return ResourceManager.GetString("EndUserApplication_PlatformRolesAssigned", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to EndUser.PlatformRolesUnassigned.
        /// </summary>
        public static string EndUserApplication_PlatformRolesUnassigned {
            get {
                return ResourceManager.GetString("EndUserApplication_PlatformRolesUnassigned", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to EndUser.TenantRolesAssigned.
        /// </summary>
        public static string EndUserApplication_TenantRolesAssigned {
            get {
                return ResourceManager.GetString("EndUserApplication_TenantRolesAssigned", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to EndUser.Registered.TermsAccepted.
        /// </summary>
        public static string EndUsersApplication_User_Registered_TermsAccepted {
            get {
                return ResourceManager.GetString("EndUsersApplication_User_Registered_TermsAccepted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Authentication.Failed.AccountLocked.
        /// </summary>
        public static string PasswordCredentialsApplication_Authenticate_AccountLocked {
            get {
                return ResourceManager.GetString("PasswordCredentialsApplication_Authenticate_AccountLocked", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Authentication.Failed.AccountSuspended.
        /// </summary>
        public static string PasswordCredentialsApplication_Authenticate_AccountSuspended {
            get {
                return ResourceManager.GetString("PasswordCredentialsApplication_Authenticate_AccountSuspended", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Authentication.Failed.BeforeVerified.
        /// </summary>
        public static string PasswordCredentialsApplication_Authenticate_BeforeVerified {
            get {
                return ResourceManager.GetString("PasswordCredentialsApplication_Authenticate_BeforeVerified", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Authentication.Failed.InvalidCredentials.
        /// </summary>
        public static string PasswordCredentialsApplication_Authenticate_InvalidCredentials {
            get {
                return ResourceManager.GetString("PasswordCredentialsApplication_Authenticate_InvalidCredentials", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Authentication.Passed.
        /// </summary>
        public static string PasswordCredentialsApplication_Authenticate_Succeeded {
            get {
                return ResourceManager.GetString("PasswordCredentialsApplication_Authenticate_Succeeded", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SingleSignOn.AutoRegistered.
        /// </summary>
        public static string SingleSignOnApplication_Authenticate_AccountOnboarded {
            get {
                return ResourceManager.GetString("SingleSignOnApplication_Authenticate_AccountOnboarded", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Authentication.Failed.AccountSuspended.
        /// </summary>
        public static string SingleSignOnApplication_Authenticate_AccountSuspended {
            get {
                return ResourceManager.GetString("SingleSignOnApplication_Authenticate_AccountSuspended", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Authentication.Passed.
        /// </summary>
        public static string SingleSignOnApplication_Authenticate_Succeeded {
            get {
                return ResourceManager.GetString("SingleSignOnApplication_Authenticate_Succeeded", resourceCulture);
            }
        }
    }
}
