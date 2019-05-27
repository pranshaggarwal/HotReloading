using System;
using FluentAssertions;
using HotReloading.Test.TestCodes;
using NUnit.Framework;

namespace HotReloading.Test
{
    [TestFixture]
    public class FieldTest
    {
        [SetUp]
        public void Setup()
        {
            Helper.Setup();
        }

        [Test]
        public void AddFieldAndAccessItFromSameClass()
        {
            var field = Helper.GetField("FieldTestClass", "field1");

            var method = Helper.GetMethod("FieldTestClass", "TestField1");

            Runtime.HandleCodeChange(new Core.CodeChange
            {
                Methods = new System.Collections.Generic.List<Core.Method>
                {
                    method
                },
                Fields = new System.Collections.Generic.List<Core.Field>
                {
                    field
                }
            });

            FieldTestClass.TestField1();

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public void AddStaticFieldAndAccessItFromSameClass()
        {

        }

        [Test]
        public void AddFieldAndAccessItFromDifferentClass()
        {

        }

        [Test]
        public void AddStaticFieldAndAccessItFromDifferentClass()
        {

        }
    }
}
