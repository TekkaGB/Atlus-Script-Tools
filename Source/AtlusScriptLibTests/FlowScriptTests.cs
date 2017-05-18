﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace AtlusScriptLib.Tests
{
    [TestClass()]
    public class FlowScriptTests
    {
        FlowScript Script;

        private void FromFileTestBase(string path, FlowScriptBinaryFormatVersion version, FlowScriptBinaryFormatVersion actualVersion)
        {
            Script = FlowScript.FromFile(path, version);

            Assert.IsNotNull(Script, "Script object should not be null");
            Assert.AreEqual(actualVersion, Script.FormatVersion);
        }

        [TestMethod()]
        public void FromFileTest_V1_KnownVersion()
        {
            FromFileTestBase("TestResources\\V1.bf", FlowScriptBinaryFormatVersion.V1, FlowScriptBinaryFormatVersion.V1);

            Assert.AreEqual(10061, Script.Instructions.Count);
            Assert.AreEqual(742, Script.JumpLabels.Count);
            Assert.AreEqual(0, Script.LocalFloatVariableCount);
            Assert.AreEqual(113, Script.LocalIntVariableCount);
            Assert.AreEqual(77521, Script.MessageScript.Length);
            Assert.AreEqual(96, Script.ProcedureLabels.Count);
            Assert.AreEqual(240, Script.Strings.Count);
            Assert.AreEqual(FlowScriptOpcode.COMM, Script.Instructions[2].Opcode);
            Assert.AreEqual(102, Script.Instructions[2].Operand.GetInt16Value());
            Assert.ThrowsException<InvalidOperationException>(() => Script.Instructions[2].Operand.GetInt32Value());
        }

        [TestMethod()]
        public void FromFileTest_V1_UnknownVersion()
        {
            FromFileTestBase("TestResources\\V1.bf", FlowScriptBinaryFormatVersion.Unknown, FlowScriptBinaryFormatVersion.V1);
        }

        [TestMethod()]
        public void FromFileTest_V1_WrongVersion()
        {
            FromFileTestBase("TestResources\\V1.bf", FlowScriptBinaryFormatVersion.V3_BE, FlowScriptBinaryFormatVersion.V1);
        }

        [TestMethod()]
        public void FromFileTest_V2_KnownVersion()
        {
            FromFileTestBase("TestResources\\V2.bf", FlowScriptBinaryFormatVersion.V2, FlowScriptBinaryFormatVersion.V2);
        }

        [TestMethod()]
        public void FromFileTest_V2_UnknownVersion()
        {
            FromFileTestBase("TestResources\\V2.bf", FlowScriptBinaryFormatVersion.Unknown, FlowScriptBinaryFormatVersion.V2);
        }

        [TestMethod()]
        public void FromFileTest_V2_WrongVersion()
        {
            FromFileTestBase("TestResources\\V2.bf", FlowScriptBinaryFormatVersion.V3_BE, FlowScriptBinaryFormatVersion.V2);
        }

        [TestMethod()]
        public void FromFileTest_V3_BE_KnownVersion()
        {
            FromFileTestBase("TestResources\\V3_BE.bf", FlowScriptBinaryFormatVersion.V3_BE, FlowScriptBinaryFormatVersion.V3_BE);
        }

        [TestMethod()]
        public void FromFileTest_V3_BE_UnknownVersion()
        {
            FromFileTestBase("TestResources\\V3_BE.bf", FlowScriptBinaryFormatVersion.Unknown, FlowScriptBinaryFormatVersion.V3_BE);
        }

        [TestMethod()]
        public void FromFileTest_V3_BE_WrongVersion()
        {
            FromFileTestBase("TestResources\\V3_BE.bf", FlowScriptBinaryFormatVersion.V1, FlowScriptBinaryFormatVersion.V3_BE);
        }

        [TestMethod()]
        public void FromFileTest_InvalidFileFormat_Small()
        {
            Assert.ThrowsException<InvalidDataException>( () => FlowScript.FromFile("TestResources\\dummy_small.bin", FlowScriptBinaryFormatVersion.Unknown) );
        }

        [TestMethod()]
        public void FromFileTest_InvalidFileFormat_Big()
        {
            Assert.ThrowsException<InvalidDataException>( () => FlowScript.FromFile("TestResources\\dummy_big.bin", FlowScriptBinaryFormatVersion.Unknown) );
        }

        [TestMethod()]
        [Ignore]
        public void FromFileTest_Batch()
        {
            foreach (var path in Directory.EnumerateFiles("TestResources\\Batch\\", "*.bf"))
            {
                var script = FlowScript.FromFile(path, FlowScriptBinaryFormatVersion.V3_BE);

                Assert.IsNotNull(script);
            }
        }

        [TestMethod()]
        public void FromStreamTest()
        {
            using (var fileStream = File.OpenRead("TestResources\\V3_BE.bf"))
            {
                var script = FlowScript.FromStream(fileStream, FlowScriptBinaryFormatVersion.V3_BE);

                Assert.IsNotNull(script);
                Assert.AreEqual(FlowScriptBinaryFormatVersion.V3_BE, script.FormatVersion);
            }
        }
    }
}