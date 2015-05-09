using System;
using System.Threading;
using FluentAssertions;
using PasswordGenerator;
using Xunit;

namespace PasswordGeneratorTests
{
    public class AccessTokenRepositoryUnitTests
    {
        private const string Key = "key";
        private const string Key2 = "key2";
        private const string Key3 = "key3";

        private readonly AccessTokenRepositoryUnitTests when;
        private readonly AccessTokenRepositoryUnitTests then;
        private readonly AccessTokenRepositoryUnitTests and;

        private readonly AccessTokenRepository _accessTokenRepository = new AccessTokenRepository(TimeSpan.FromSeconds(10));

        private string _value;

        public AccessTokenRepositoryUnitTests()
        {
            when = then = and = this;
        }

        [Fact]
        public void when_no_elements_should_return_null()
        {
            when.requesting_for_element(Key);
            then.repository_returns_null();
        }

        private void repository_returns_null()
        {
            _value.Should().BeNull();
        }

        private void requesting_for_element(string key)
        {
            _value = _accessTokenRepository.Retrieve(key);
        }

        [Fact]
        public void when_element_not_added_should_return_null()
        {
            when.elements_added();
            and.requesting_for_element(Key2);
            then.repository_returns_null();
        }

        private void elements_added()
        {
            _accessTokenRepository.Add(Key, Key + "at");
            _accessTokenRepository.Add(Key3, Key3 + "at");
        }

        [Fact]
        public void when_element_added_should_return_value()
        {
            when.elements_added();
            and.requesting_for_element(Key);
            then.repository_returns_value(Key);
        }

        private void repository_returns_value(string userId)
        {
            _value.Should().Be(userId + "at");
        }

        [Fact]
        public void when_requesting_for_element_before_expired_should_return_value()
        {
            when.elements_added();
            and.wait(5000);
            and.requesting_for_element(Key);
            then.repository_returns_value(Key);
        }

        private void wait(int miliseconds)
        {
            Thread.Sleep(miliseconds);
        }

        [Fact]
        public void when_requesting_for_element_after_expired_should_return_null()
        {
            when.elements_added();
            and.wait(10010);
            and.requesting_for_element(Key);
            then.repository_returns_null();
        }


    }
}
