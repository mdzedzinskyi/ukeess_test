﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Models;
using Models.Contacts;

namespace BLL.DataAccess
{
    public interface IUserRepositoryProxy : IDisposable
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        void InsertUser(User item);
        void UpdateUser(User item);
        void DeleteUser(int id);

        void InsertUserContact(int userId, Contact contact);
        void DeleteUserContact(int userId, int contactId, int contactType);
        void UpdateUserContact(Contact contact);
    }
}