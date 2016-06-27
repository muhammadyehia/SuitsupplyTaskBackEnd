﻿using System;
using System.Data.Entity.Migrations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace SuitsupplyTask.IntegrationTest
{
    [SetUpFixture]
    public class GlobalSetup
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            var configuration = new Infrastructure.Migrations.Configuration();
            var migrator = new DbMigrator(configuration);
            migrator.Update();
        }
    }
}
