namespace eCommerceBase.Domain.SeedWork
{
    public interface IUser
    {
        public Guid Id { get;  }
        public string Email { get;  }
        public string FirstName { get;  }
        public string LastName { get;  }
        public Guid UserGroupId { get; }
    }
}
