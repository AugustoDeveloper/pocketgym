using PocketGym.Domain.Core.Entities;
using PocketGym.Domain.Repositories;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketGym.Infrastructure.Repository.RavenDb
{
    public class UserRepository : RavenDbRepository<User>, IUserRepository
    {
        public UserRepository(IAsyncDocumentSession session) : base(session)
        {
        }

        protected override void InternalUpdate(User newEntity, User registered)
        {
            registered.Name = newEntity.Name;
            registered.Age = newEntity.Age;;
        }
    }
}