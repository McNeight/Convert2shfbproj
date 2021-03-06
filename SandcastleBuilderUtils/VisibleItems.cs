//===============================================================================================================
// System  : Sandcastle Help File Builder Utilities
// File    : VisibleItems.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/03/2013
// Note    : Copyright 2006-2013, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the enumerated type that defines the optional visible items in the help file
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code.  It can also be found at the project website: https://GitHub.com/EWSoftware/SHFB.  This
// notice, the author's name, and all copyright notices must remain intact in all applications, documentation,
// and source files.
//
// Version     Date     Who  Comments
// ==============================================================================================================
// 1.3.1.0  10/03/2006  EFW  Created the code
// 1.5.1.0  08/24/2007  EFW  Added support for the inherited private/internal framework member flags
// 1.9.9.0  12/03/2013  EFW  Added support for no-PIA embedded interop types
//===============================================================================================================

using System;

namespace SandcastleBuilder.Utils
{
    /// <summary>
    /// This public enumerated type defines the optional visible items in the help file
    /// </summary>
    [Flags, Serializable]
    public enum VisibleItems
    {
        /// <summary>None of the optional items are documented.  Only public types and members will be documented.</summary>
        None                              = 0x0000,
        /// <summary>Document attributes.</summary>
        Attributes                        = 0x0001,
        /// <summary>Document explicit interface implementations.</summary>
        ExplicitInterfaceImplementations  = 0x0002,
        /// <summary>Document inherited members.</summary>
        InheritedMembers                  = 0x0004,
        /// <summary>Document inherited framework members.  For this to work, <c>InheritedMembers</c> must also
        /// be enabled.</summary>
        InheritedFrameworkMembers         = 0x0008,
        /// <summary>Document inherited internal framework members.  For this to work <c>InheritedFrameworkMembers</c>
        /// must also be enabled.</summary>
        InheritedFrameworkInternalMembers = 0x0010,
        /// <summary>Document inherited private framework members.  For this to work <c>InheritedFrameworkMembers</c>
        /// must also be enabled.</summary>
        InheritedFrameworkPrivateMembers  = 0x0020,
        /// <summary>Document internal members.</summary>
        Internals                         = 0x0040,
        /// <summary>Document private members.</summary>
        Privates                          = 0x0080,
        /// <summary>Document private fields.  For this to work, <c>Privates</c> must also be enabled.</summary>
        PrivateFields                     = 0x0100,
        /// <summary>Document protected members.</summary>
        Protected                         = 0x0200,
        /// <summary>Document protected members of sealed classes.  For this to work, <c>Protected</c> must also
        /// be enabled.</summary>
        SealedProtected                   = 0x0400,
        /// <summary>Document "protected internal" members as "protected" only.</summary>
        ProtectedInternalAsProtected      = 0x0800,
        /// <summary>Document no-PIA (Primary Interop Assembly) embedded COM types.</summary>
        NoPIATypes                        = 0x1000
    }
}
