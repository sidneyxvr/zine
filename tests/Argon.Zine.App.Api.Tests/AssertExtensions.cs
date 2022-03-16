using FluentAssertions.Primitives;
using System.IO;
using System.Net;
using System.Net.Http;

namespace Argon.Zine.App.Api.Tests;

internal static class AssertExtensions
{
    public static AndConstraint<TAssertions> BeHttpResponseOkOrLogError<TSubject, TAssertions>(
        this ObjectAssertions<TSubject, TAssertions> objectAssertions, Action<string>? logTo = null)
        where TSubject : HttpResponseMessage
        where TAssertions : ObjectAssertions<TSubject, TAssertions>
    {
        if (objectAssertions.Subject.StatusCode != HttpStatusCode.OK && logTo is not null)
        {
            using var memoryStream = (MemoryStream)objectAssertions.Subject.Content.ReadAsStream();

            using var streamReader = new StreamReader(memoryStream);

            logTo(streamReader.ReadToEnd());
        }
        objectAssertions.Subject.StatusCode.Should().Be(HttpStatusCode.OK);

        return new AndConstraint<TAssertions>((TAssertions)objectAssertions);
    }

    public static AndConstraint<TAssertions> BeHttpResponseBadRequestOrLogError<TSubject, TAssertions>(
        this ObjectAssertions<TSubject, TAssertions> objectAssertions, Action<string>? logTo = null)
        where TSubject : HttpResponseMessage
        where TAssertions : ObjectAssertions<TSubject, TAssertions>
    {
        if (objectAssertions.Subject.StatusCode != HttpStatusCode.BadRequest && logTo is not null)
        {
            using var memoryStream = (MemoryStream)objectAssertions.Subject.Content.ReadAsStream();

            using var streamReader = new StreamReader(memoryStream);

            logTo(streamReader.ReadToEnd());
        }
        objectAssertions.Subject.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        return new AndConstraint<TAssertions>((TAssertions)objectAssertions);
    }
}