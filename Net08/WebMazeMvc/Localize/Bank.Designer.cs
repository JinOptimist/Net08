﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebMazeMvc.Localize {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Bank {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Bank() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WebMazeMvc.Localize.Bank", typeof(Bank).Assembly);
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
        ///   Looks up a localized string similar to Add Bank.
        /// </summary>
        public static string AllBanks_addBank {
            get {
                return ResourceManager.GetString("AllBanks_addBank", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Delete.
        /// </summary>
        public static string AllBanks_delete {
            get {
                return ResourceManager.GetString("AllBanks_delete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to List of clients.
        /// </summary>
        public static string AllBanks_listOfClients {
            get {
                return ResourceManager.GetString("AllBanks_listOfClients", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Back to the list of banks.
        /// </summary>
        public static string AllClientsOfBank_BackToBanks {
            get {
                return ResourceManager.GetString("AllClientsOfBank_BackToBanks", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Add bank.
        /// </summary>
        public static string BanksAdding_AddBankButton {
            get {
                return ResourceManager.GetString("BanksAdding_AddBankButton", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Back to the list of banks.
        /// </summary>
        public static string BanksAdding_BackToBanks {
            get {
                return ResourceManager.GetString("BanksAdding_BackToBanks", resourceCulture);
            }
        }
    }
}