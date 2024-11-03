﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IdentityApplication {
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
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("IdentityApplication.Resources", typeof(Resources).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The user account is suspended.
        /// </summary>
        internal static string APIKeysApplication_AccountSuspended {
            get {
                return ResourceManager.GetString("APIKeysApplication_AccountSuspended", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The user account is suspended.
        /// </summary>
        internal static string AuthTokensApplication_AccountSuspended {
            get {
                return ResourceManager.GetString("AuthTokensApplication_AccountSuspended", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The user account is locked out.
        /// </summary>
        internal static string PasswordCredentialsApplication_AccountLocked {
            get {
                return ResourceManager.GetString("PasswordCredentialsApplication_AccountLocked", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The user account is suspended.
        /// </summary>
        internal static string PasswordCredentialsApplication_AccountSuspended {
            get {
                return ResourceManager.GetString("PasswordCredentialsApplication_AccountSuspended", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The user account ID is not a valid identifier.
        /// </summary>
        internal static string PasswordCredentialsApplication_InvalidUserAccountId {
            get {
                return ResourceManager.GetString("PasswordCredentialsApplication_InvalidUserAccountId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The username is not a valid email address.
        /// </summary>
        internal static string PasswordCredentialsApplication_InvalidUsername {
            get {
                return ResourceManager.GetString("PasswordCredentialsApplication_InvalidUsername", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Authentication requires another factor.
        /// </summary>
        internal static string PasswordCredentialsApplication_MfaRequired {
            get {
                return ResourceManager.GetString("PasswordCredentialsApplication_MfaRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The user account ID is not a person.
        /// </summary>
        internal static string PasswordCredentialsApplication_NotPerson {
            get {
                return ResourceManager.GetString("PasswordCredentialsApplication_NotPerson", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The user account has already been verified.
        /// </summary>
        internal static string PasswordCredentialsApplication_RegistrationAlreadyVerified {
            get {
                return ResourceManager.GetString("PasswordCredentialsApplication_RegistrationAlreadyVerified", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The user account has not been verified.
        /// </summary>
        internal static string PasswordCredentialsApplication_RegistrationNotVerified {
            get {
                return ResourceManager.GetString("PasswordCredentialsApplication_RegistrationNotVerified", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The user account is suspended.
        /// </summary>
        internal static string SingleSignOnApplication_AccountSuspended {
            get {
                return ResourceManager.GetString("SingleSignOnApplication_AccountSuspended", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The &apos;{0}&apos; is not registered.
        /// </summary>
        internal static string SSOProvidersService_UnknownProvider {
            get {
                return ResourceManager.GetString("SSOProvidersService_UnknownProvider", resourceCulture);
            }
        }
    }
}
