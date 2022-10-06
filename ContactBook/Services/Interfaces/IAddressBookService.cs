﻿using System;
using ContactBook.Models;

namespace ContactBook.Services.Interfaces
{
    public interface IAddressBookService
    {
        Task AddContactToACategoryAsync(int categoryId, int contactId);

        Task<bool> IsContactInCategory(int categoryId, int contactId);

        Task<IEnumerable<Category>> GetUserCategoriesAsync(string userId);

        Task<ICollection<int>> GetContactCategoryIdsAsync(int contactId);

        Task<ICollection<Category>> GetContactCategoriesAsync(int contactId);

        Task RemoveContactFromCategoryAsync(int categoryId, int contactId);

        IEnumerable<Contact> SearchForContacts(string searchString, string userId);
    }
}

