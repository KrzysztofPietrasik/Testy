using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Projects.UnitTests
{
    [TestFixture]
    public class StackTests
    {
        private Stack<string> _stack;

        [SetUp]
        public void SetUp()
        {
            _stack = new Stack<string>();
        }

        [Test]
        public void Push_AddingNull_ThrowsArgumentNullException()
        {
            Assert.That(() => _stack.Push(null), Throws.ArgumentNullException);
        }

        [Test]
        public void Push_AddingValidObject_AddTheObjectToStack()
        {
            _stack.Push("cos");
            Assert.That(_stack.Count, Is.EqualTo(1));
        }

        [Test]
        public void Count_EmptyStack_ReturnsZero()
        {
            Assert.That(_stack.Count, Is.EqualTo(0));
        }

        [Test]
        public void Pop_EmptyStack_ThrowsInvalidOperationException()
        {
            Assert.That(() => _stack.Pop(), Throws.InvalidOperationException);
        }

        [Test]
        public void Pop_StackWithOneObject_ReturnsObjectOnTop()
        {
            _stack.Push("cos");
            _stack.Pop();

            Assert.That(() => _stack.Count, Is.EqualTo(0));
        }

        [Test]
        public void Pop_StackWithAFewObjects_RemovesObjectOnTop()
        {
            _stack.Push("cos");
            _stack.Push("chyba");
            _stack.Push("nie");
            _stack.Push("dziala");
            _stack.Pop();

            Assert.That(_stack.Count, Is.EqualTo(3));
        }

        [Test]
        public void Peek_EmptyStack_ThrowsInvalidOperationException()
        {
            Assert.That(() => _stack.Peek(), Throws.InvalidOperationException);
        }

        [Test]
        public void Peek_StackWithTwoObjects_ReturnsObjectOnTop()
        {
            _stack.Push("kot");
            _stack.Push("pies");       

            Assert.That(() => _stack.Peek(), Is.EqualTo("pies"));
        }
    }
}
