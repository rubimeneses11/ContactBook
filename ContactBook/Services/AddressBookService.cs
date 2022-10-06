using System;
using System.Linq;
using ContactBook.Data;
using ContactBook.Models;
using ContactBook.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContactBook.Services
{
    public class AddressBookService : IAddressBookService
    {
        //private variable
        private readonly ApplicationDbContext _context;

        //constructor
        public AddressBookService(ApplicationDbContext context)

        {
            _context = context;
        }

        public async Task AddContactToACategoryAsync(int categoryId, int contactId)
        {
            try
            {
                //check if category passed in is in the contact already
                //to do this we use our method IsContactInCategory
                if(!await IsContactInCategory(categoryId, contactId))
                {
                    //find statements
                    Contact? contact = await _context.Contacts.FindAsync(contactId);

                    Category? category = await _context.Categories.FindAsync(categoryId);

                    //add to db if both items are filled in
                    if(category != null && contact != null)
                    {
                        category.Contacts.Add(contact);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<ICollection<Category>> GetContactCategoriesAsync(int contactId)
        {
            try
            {
                Contact? contact = await _context.Contacts.Include(c => c.Categories).FirstOrDefaultAsync(c => c.Id == contactId);
                return contact.Categories;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ICollection<int>> GetContactCategoryIdsAsync(int contactId)
        {
            try
            {
                var contact = await _context.Contacts.Include(c => c.Categories)
                                                   .FirstOrDefaultAsync(c => c.Id == contactId);

                List<int> categoryIds = contact.Categories.Select(c => c.Id).ToList();

                return categoryIds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Category>> GetUserCategoriesAsync(string userId)
        {
            List<Category> categories = new List<Category>();

            try
            {
                categories = await _context.Categories.Where(x => x.AppUserId == userId)
                                                      .OrderBy(x => x.Name)
                                                      .ToListAsync();
            }
            catch
            {
                throw;
            }

            return categories;
        }

        public async Task<bool> IsContactInCategory(int categoryId, int contactId)
        {
            //can we find the contact, first
            Contact? contact = await _context.Contacts.FindAsync(contactId);

            return await _context.Categories
                                 .Include(c => c.Contacts)
                                 .Where(c => c.Id == categoryId && c.Contacts.Contains(contact)) //makes sure we get the right category and that the contact is in the category
                                 .AnyAsync(); //returns true or false
        }

        public async Task RemoveContactFromCategoryAsync(int categoryId, int contactId)
        {
            try
            {
                if (await IsContactInCategory(categoryId, contactId))
                {
                    Contact contact = await _context.Contacts.FindAsync(contactId);
                    Category category = await _context.Categories.FindAsync(categoryId);

                    if(category != null && contact != null)
                    {
                        category.Contacts.Remove(contact);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Contact> SearchForContacts(string searchString, string userId)
        {
            throw new NotImplementedException();
        }
    }
}

