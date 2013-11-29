using System;
using Domain.Commands;
using Domain.Model.Part;
using Insight.Cqrs.Configuration;
using Insight.Cqrs.ReadOnlyStorage;
using Insight.Cqrs.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace _123.Tests
{
    [Binding]
    public class AddPartSteps
    {
        PartDto newPart;
        private Guid tmpGuid;
        private IRepository<Part> irepo;

        [Given(@"a user has entered information about a Part")] 
        public void GivenAUserHasEnteredInformationAboutAPart() 
        {
            tmpGuid = Guid.NewGuid();
            newPart = new PartDto();
            newPart.Id = tmpGuid;
            newPart.PartDescription = "PC";
            newPart.PartNumber = "PC/1000";
            newPart.UnitOfMeasure = 1;
            newPart.SalesLeadTime = 5;
        }
        
        [Then(@"that Part should be stored in the system")]
        public void ThenThatPartShouldBeStoredInTheSystem()
        {
            ServiceLocator.CommandBus.Send(new CreatePartCommand(newPart.Id, newPart.PartNumber, newPart.PartDescription, newPart.UnitOfMeasure, newPart.SalesLeadTime));
            Assert.IsNotNull(ServiceLocator.ReportDatabase.GetById(tmpGuid));
        }
        
        [When(@"user user change the Description")]
        public void WhenUserUserChangeTheDescription()
        {
            ServiceLocator.CommandBus.Send(new ChangePartDescriptionCommand(tmpGuid,"This has been changed"));
        }
        [Then(@"the description of that part should be updated")]
        public void ThenTheDescriptionOfThatPartShouldBeUpdated()
        {
            Assert.AreEqual("This has been changed", ServiceLocator.ReportDatabase.GetById(tmpGuid).PartDescription);
        }
        
        [When(@"the user change the UnitOfMeasure")]
        public void WhenTheUserChangeTheUnitOfMeasure()
        {
            ServiceLocator.CommandBus.Send(new ChangePartDescriptionCommand(tmpGuid, "This has been changed twice"));
        }
        [Then(@"the UnitOfMeasure of that part should be update")]
        public void ThenTheUnitOfMeasureOfThatPartShouldBeUpdate()
        {
            Assert.AreEqual("This has been changed twice", ServiceLocator.ReportDatabase.GetById(tmpGuid).PartDescription);
        }
    }
}
