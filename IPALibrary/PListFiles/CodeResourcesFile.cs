/* Copyright (C) 2017 ROM Knowledgeware. All rights reserved.
 * 
 * You can redistribute this program and/or modify it under the terms of
 * the GNU Lesser Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version.
 * 
 * Maintainer: Tal Aloni <tal@kmrom.com>
 */
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;
using PListNet;
using PListNet.Nodes;

namespace IPALibrary
{
    public class CodeResourcesFile : PListFile
    {
        public CodeResourcesFile(byte[] codeResourcesBytes) : base(codeResourcesBytes)
        {
        }

        public byte[] GetFileHash(string fileName)
        {
            DictionaryNode filesNode = PListHelper.GetDictionaryValueFromPList(RootNode, "files");
            return PListHelper.GetDataValueFromPList(filesNode, fileName);
        }

        public void UpdateFileHash(string fileName, byte[] fileBytes)
        {
            DictionaryNode filesNode = PListHelper.GetDictionaryValueFromPList(RootNode, "files");
            byte[] hash = new SHA1Managed().ComputeHash(fileBytes);
            if (filesNode.ContainsKey(fileName))
            {
                filesNode[fileName] = new DataNode(hash);
            }

            DictionaryNode files2Node = PListHelper.GetDictionaryValueFromPList(RootNode, "files2");
            if (files2Node != null && files2Node.ContainsKey(fileName))
            {
                files2Node[fileName] = new DataNode(hash);
            }
        }
    }
}
