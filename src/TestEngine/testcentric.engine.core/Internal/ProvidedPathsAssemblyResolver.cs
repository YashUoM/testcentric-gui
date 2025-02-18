// ***********************************************************************
// Copyright (c) Charlie Poole and TestCentric GUI contributors.
// Licensed under the MIT License. See LICENSE file in root directory.
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using NUnit.Engine;

namespace TestCentric.Engine.Internal
{
    public class ProvidedPathsAssemblyResolver
    {
        static ILogger log = InternalTrace.GetLogger(typeof(ProvidedPathsAssemblyResolver));

        public ProvidedPathsAssemblyResolver()
        {
            _resolutionPaths = new List<string>();
        }

        public void Install()
        {
            Debug.Assert(AppDomain.CurrentDomain.IsDefaultAppDomain());
            AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolve;
        }

        public void AddPath(string dirPath)
        {
            if (!_resolutionPaths.Contains(dirPath))
            {
                _resolutionPaths.Add(dirPath);
                log.Debug("Added path " + dirPath);
            }
        }

        public void AddPathFromFile(string filePath)
        {
            string dirPath = Path.GetDirectoryName(filePath);
            AddPath(dirPath);
        }

        public void RemovePath(string dirPath)
        {
            _resolutionPaths.Remove(dirPath);
        }

        public void RemovePathFromFile(string filePath)
        {
            string dirPath = Path.GetDirectoryName(filePath);
            RemovePath(dirPath);
        }

        Assembly AssemblyResolve(object sender, ResolveEventArgs args)
        {
            foreach (string path in _resolutionPaths)
            {
                string filename = new AssemblyName(args.Name).Name + ".dll";
                string fullPath = Path.Combine(path, filename);
                try
                {
                    if (File.Exists(fullPath))
                    {
                        return Assembly.LoadFrom(fullPath);
                    }
                }
                catch (Exception)
                {
                    // Resolution at this path failed. Do not interrupt the process; try the next path.
                }
            }

            return null;
        }

        List<string> _resolutionPaths;
    }
}
