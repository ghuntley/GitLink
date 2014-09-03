﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PdbInfo.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2014 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace GitLink.Pdb
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PdbInfo
    {
        public PdbInfo()
        {
            Guid = new Guid();
            StreamToPdbName = new SortedDictionary<int, PdbName>();
            NameToPdbName = new SortedDictionary<string, PdbName>();
            FlagIndexToPdbName = new SortedDictionary<int, PdbName>(FlagIndexToPdbName);
            FlagIndexes = new SortedSet<int>();
            SrcSrv = new string[0];
            Tail = new byte[0];
        }

        public int Version { get; set; }
        public int Signature { get; set; }
        public Guid Guid { get; set; }
        public int Age { get; set; }
        public int FlagIndexMax { get; set; }
        public int FlagCount { get; set; }
        public IDictionary<int, PdbName> StreamToPdbName { get; set; }
        public IDictionary<string, PdbName> NameToPdbName { get; set; }
        public IDictionary<int, PdbName> FlagIndexToPdbName { get; set; }
        public SortedSet<int> FlagIndexes { get; set; }
        public string[] SrcSrv { get; set; }
        public byte[] Tail { get; set; }

        public void ClearFlags()
        {
            FlagIndexes.Clear();
            FlagIndexToPdbName.Clear();
        }

        public void AddFlag(PdbName name)
        {
            FlagIndexes.Add(name.FlagIndex);
            FlagIndexToPdbName.Add(name.FlagIndex, name);
        }

        public void AddName(PdbName name)
        {
            StreamToPdbName.Add(name.Stream, name);
            NameToPdbName.Add(name.Name, name);
            AddFlag(name);
        }

        public PdbName AddNewName(string name)
        {
            var pdbName = new PdbName();
            pdbName.Name = name;
            var streamNumbers = StreamToPdbName.Keys.ToArray();
            var lastStream = streamNumbers[streamNumbers.Length - 1];
            pdbName.Stream = lastStream + 1;
            AddName(pdbName);
            return pdbName;
        }
    }
}