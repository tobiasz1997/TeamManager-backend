using Microsoft.AspNetCore.Identity;
using TeamManager.Core.Assignments.Models;
using TeamManager.Core.Timers.Models;
using TeamManager.Core.Users.Enums;
using TeamManager.Core.Users.Models;
using TeamManager.Infrastructure.Shared.Security;
using Timer = TeamManager.Core.Timers.Models.Timer;

namespace TeamManager.Tests.Integration.Shared;

internal static class CreateModels
{
    public static async Task<User> CreateUser(TestDatabase database, string email = "default@default.pl", string password = "Default123")
    {

        var passwordService = new PasswordService(new PasswordHasher<User>());
        var user = new User(Guid.NewGuid(), email, passwordService.Secure(password), "Jan", "Kowalski", DateTime.UtcNow);

        await database.DbContext.User.AddAsync(user);
        await database.DbContext.SaveChangesAsync();

        return user;
    } 
    
    public static async Task<Project> CreateProject(TestDatabase database, Guid userId)
    {
        var projectId = Guid.NewGuid();
        var project = new Project(projectId, userId, "Default", "#000", DateTime.UtcNow);
        await database.DbContext.Projects.AddAsync(project);
        await database.DbContext.SaveChangesAsync();

        return project;
    }
    
    public static async Task<Assignment> CreateAssignment(TestDatabase database, Guid userId, AssignmentStatusType status = AssignmentStatusType.ToDo)
    {
        var assignmentId = Guid.NewGuid();
        var assignment = new Assignment(assignmentId, userId, "Default Name", "Default Description", 1, status,
            DateTime.UtcNow);
        await database.DbContext.Assignments.AddAsync(assignment);
        await database.DbContext.SaveChangesAsync();

        return assignment;
    }    
    
    public static async Task<Timer> CreateTimer(TestDatabase database, Guid userId)
    {
        var timerId = Guid.NewGuid();

        var timer = new Timer(timerId, userId, null, "Default", new DateTime(2022,01,01), DateTime.UtcNow);
        await database.DbContext.Timers.AddAsync(timer);
        await database.DbContext.SaveChangesAsync();

        return timer;
    }
}