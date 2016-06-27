using System;
using System.Transactions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace SuitsupplyTask.IntegrationTest
{
    public class Isolated : Attribute, ITestAction
    {
        private TransactionScope _transactionScope;
        public ActionTargets Targets
        {
            get
            {
               return  ActionTargets.Test;
            }
        }

        public void AfterTest(ITest test)
        {
            _transactionScope.Dispose();
        }

        public void BeforeTest(ITest test)
        {
            _transactionScope=new TransactionScope();
        }
    }
}
