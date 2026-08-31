using Acelera.Core.Models;
using System.Text.Json.Serialization;

namespace Acelera.Core.Abstractions;

/// <summary>
/// The base user's profile
/// </summary>
[JsonDerivedType(typeof(StudentProfile), typeDiscriminator: "student")]
[JsonDerivedType(typeof(InstructorProfile), typeDiscriminator: "instructor")]
public abstract class ProfileBase { }
