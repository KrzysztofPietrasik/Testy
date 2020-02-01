using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Projects.House;

namespace Projects.UnitTests
{
    [TestFixture]
    public class HousekeeperServiceTests
    {
        private HousekeeperService _service;
        private Mock<IStatementGenerator> _statementGenerator;
        private Mock<IEmailSender> _emailSender;
        private Mock<IXtraMessageBox> _messageBox;
        private DateTime _statementDate = new DateTime(2017, 1, 1);
        private Housekeeper _houseKeeper;
        private string _statementFileName = "filename";

        [SetUp]
        public void SetUp()
        {
            _houseKeeper = new Housekeeper
            {
                Email = "asd",
                FullName = "sada",
                Oid = 1,
                StatementEmailBody = "dasdw"
            };

            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(uow => uow.Query<Housekeeper>()).Returns(new List<Housekeeper>
            {
                _houseKeeper
            }.AsQueryable());

            _statementGenerator = new Mock<IStatementGenerator>();
            _emailSender = new Mock<IEmailSender>();
            _messageBox = new Mock<IXtraMessageBox>();


            _service = new HousekeeperService(
                unitOfWork.Object,
                _statementGenerator.Object,
                _emailSender.Object,
                _messageBox.Object);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            _service.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(sg =>
                sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate));
        }

        [Test]
        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("")]
        public void SendStatementEmails_HouseKeepersEmailIsNullOrIllegalString_ShouldNotGenerateStatements(string emailAddress)
        {
            _houseKeeper.Email = emailAddress;

            _service.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(foo =>
                foo.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate),
                Times.Never);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_EmailTheStatement()
        {
            _statementGenerator
                .Setup(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate))
                .Returns(_statementFileName);

            _service.SendStatementEmails(_statementDate);

            _emailSender.Verify(es => es.EmailFile(
                _houseKeeper.Email,
                _houseKeeper.StatementEmailBody,
                _statementFileName,
                It.IsAny<string>()));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void SendStatementEmails_StatementFileNameIsNullOrIllegalString_ShouldNotEmailTheStatement(string returnValue)
        {
            _statementGenerator
                .Setup(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate))
                .Returns(() => returnValue);

            _service.SendStatementEmails(_statementDate);

            VerifyEmailNotSent();
        }

        private void VerifyEmailNotSent()
        {
            _emailSender.Verify(es => es.EmailFile(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Never);
        }
    }
}
