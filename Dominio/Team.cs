﻿namespace Dominio;

public class Team
{
    public Team()
    {
        Panels = new List<Panel>();
        TeamMembers = new List<User>();
        MembersCount = 0;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedOn { get; set; }
    public string TasksDescription { get; set; }
    public int MaxUsers { get; set; }
    public List<Panel> Panels { get; set; }
    public int MembersCount { get; set; }
    public List<User> TeamMembers { get; set; }

    public bool TeamValidation()
    {
        var nameNotNull = !string.IsNullOrEmpty(Name);
        var createdOnNotNull = CreatedOn != null;
        var createdOnValid = CreatedOn <= DateTime.Now;
        var maxUsersNotCero = MaxUsers > 0;
        var panelsNotNull = Panels != null;
        var membersCount = MembersCount > 0;

        return nameNotNull && createdOnNotNull && createdOnValid &&
            maxUsersNotCero && panelsNotNull && membersCount;
    }
}