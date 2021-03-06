//===============================================================================================================
// System  : Sandcastle Help File Builder Utilities
// File    : BuildProcess.MSHelp2.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 08/24/2014
// Note    : Copyright 2008-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the code used to modify the MS Help 2 collection files.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code.  It can also be found at the project website: https://GitHub.com/EWSoftware/SHFB.  This
// notice, the author's name, and all copyright notices must remain intact in all applications, documentation,
// and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/21/2008  EFW  Created the code
//===============================================================================================================

using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace SandcastleBuilder.Utils.BuildEngine
{
    partial class BuildProcess
    {
        #region Private data members
        //=====================================================================

        private static Regex reRemoveDTD = new Regex("(<\\s*!DOCTYPE \\w+)(\\s*SYSTEM\\s*\".*?\")(>)",
            RegexOptions.IgnoreCase);
        #endregion

        /// <summary>
        /// This is used to clean up the MS Help 2 collection files so that they are ready for use in registering
        /// the collection.
        /// </summary>
        private void CleanUpCollectionFiles()
        {
            XmlDocument document;
            XmlNode node;
            string extension, tocDoc, iniFile;

            this.ReportProgress("Cleaning up collection files...");

            foreach(string file in Directory.EnumerateFiles(outputFolder, this.ResolvedHtmlHelpName + "*.Hx?"))
            {
                extension = Path.GetExtension(file).ToLowerInvariant();

                switch(extension)
                {
                    case ".hxc":
                        document = BuildProcess.OpenCollectionFile(file);

                        // Remove the compiler options
                        node = document.SelectSingleNode("HelpCollection/CompilerOptions");

                        if(node != null)
                            node.ParentNode.RemoveChild(node);
                        break;

                    case ".hxt":
                        // We don't need the whole TOC so recreate it from
                        // this string.
                        tocDoc = this.TransformText(
                            "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" +
                            "<!DOCTYPE HelpTOC>\r\n" +
                            "<HelpTOC DTDVersion=\"1.0\" LangId=\"{@LangId}\" " +
                            "PluginStyle=\"{@CollectionTocStyle}\" " +
                            "PluginTitle=\"{@HtmlEncHelpTitle}\">\r\n" +
                            "    <HelpTOCNode NodeType=\"TOC\" " +
                            "Url=\"{@HTMLEncHelpName}\" />\r\n" +
                            "</HelpTOC>\r\n");

                        document = new XmlDocument();
                        document.LoadXml(tocDoc);
                        break;

                    case ".hxk":
                        document = BuildProcess.OpenCollectionFile(file);
                        break;

                    default:
                        // Ignore it (i.e. .HXS)
                        document = null;
                        break;
                }

                if(document != null)
                    document.Save(file);
            }

            this.ReportProgress("Creating H2Reg.ini file...");

            iniFile = outputFolder + this.ResolvedHtmlHelpName + "_H2Reg.ini";

            if(File.Exists(iniFile))
                File.Delete(iniFile);

            this.TransformTemplate("Help2x_H2Reg.ini", templateFolder, outputFolder);
            File.Move(outputFolder + "Help2x_H2Reg.ini", iniFile);
        }

        /// <summary>
        /// Open the specified collection file and return it as an <see cref="XmlDocument"/> ready for editing
        /// </summary>
        /// <param name="file">The file to open</param>
        /// <remarks>The DTD is removed before returning it.</remarks>
        private static XmlDocument OpenCollectionFile(string file)
        {
            XmlDocument doc;
            Encoding enc = Encoding.Default;
            string content = BuildProcess.ReadWithEncoding(file, ref enc);

            // Get rid of the DTD declaration
            content = reRemoveDTD.Replace(content, "$1$3");

            doc = new XmlDocument();
            doc.LoadXml(content);

            return doc;
        }
    }
}
