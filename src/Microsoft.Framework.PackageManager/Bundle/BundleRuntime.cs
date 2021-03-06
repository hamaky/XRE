// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.Versioning;
using System.Security.Cryptography;

namespace Microsoft.Framework.PackageManager.Bundle
{
    public class BundleRuntime
    {
        private readonly FrameworkName _frameworkName;
        private readonly string _runtimePath;

        public BundleRuntime(BundleRoot root, FrameworkName frameworkName, string runtimePath)
        {
            _frameworkName = frameworkName;
            _runtimePath = runtimePath;
            Name = new DirectoryInfo(_runtimePath).Name;
            TargetPath = Path.Combine(root.TargetPackagesPath, Name);
        }

        public string Name { get; private set; }
        public string TargetPath { get; private set; }
        public FrameworkName Framework { get { return _frameworkName; } }

        public void Emit(BundleRoot root)
        {
            root.Reports.Quiet.WriteLine("Bundling runtime {0}", Name);

            if (Directory.Exists(TargetPath))
            {
                root.Reports.Quiet.WriteLine("  {0} already exists.", TargetPath);
                return;
            }

            if (!Directory.Exists(TargetPath))
            {
                Directory.CreateDirectory(TargetPath);
            }

            new BundleOperations().Copy(_runtimePath, TargetPath);
        }
    }
}