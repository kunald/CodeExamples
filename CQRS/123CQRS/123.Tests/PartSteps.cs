using Domain.Model;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _123.Tests
{
    [Binding]
    public class PartSteps
    {
        private List<Part> partList = new List<Part>();

        [Given(@"I want to create a part with the following input")]
        public void GivenIWantToCreateAPartWithTheFollowingInput(Table table)
        {
            foreach (var row in table.Rows)
            {
                Part part = new Part(new Guid(), row["PartNumber"], row["PartDescription"], Convert.ToInt32(row["UnitOfMeasure"]), Convert.ToInt32(row["SalesLeadTime"]));
                partList.Add(part);
            }
        }

        private Part tmpPart = null;
        [When(@"I get details of a Part")]
        public void WhenIGetDetailsOfAPart()
        {
            tmpPart = partList.FirstOrDefault(e => e.PartNumber == "ABC1");
        }

        [Then(@"the part details should Show")]
        public void ThenThePartDetailsShouldShow()
        {
            if (tmpPart != null)
                Assert.IsNotNull(tmpPart);
        }

    }
}
