using Imenik_JN.Server.Data.Interfaces;
using Imenik_JN.Server.Entities;
using Imenik_JN.Server.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Imenik_JN.Server.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IPhoneRepository _phoneRepository;
        private readonly IEmailRepository _emailRepository;

        public UserController(IUserRepository userRepository, ITagRepository tagRepository, IPhoneRepository phoneRepository, IEmailRepository emailRepository)
        {
            _userRepository = userRepository;
            _tagRepository = tagRepository;
            _phoneRepository = phoneRepository;
            _emailRepository = emailRepository;
        }

        [HttpGet]
        public IActionResult Read()
        {
            try
            {
                IQueryable<User> allUsers = _userRepository.AllIncluding(inc => inc.TagCollection, inc => inc.PhoneCollection, inc => inc.EmailCollection);
                return new OkObjectResult(allUsers);
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpGet("{id}")]
        public IActionResult Read(int id)
        {
            try
            {
                if (id < 0)
                    return BadRequest();

                User user = _userRepository.AllIncluding(inc => inc.TagCollection, inc => inc.PhoneCollection, inc => inc.EmailCollection).Where(w => w.Id == id)?.FirstOrDefault();

                return new OkObjectResult(user);
            }
            catch
            {
                return BadRequest();
            }
        }
      

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            try
            {
                if (user == null)
                    return BadRequest();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState.GetModelErrors());

                //User user = MapData<User, UserVM>(userVM);

                await SaveUser(user);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] User user)
        {
            try
            {
                if (user == null)
                    return BadRequest();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState.GetModelErrors());

                await UpdateUser(user);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] List<User> userList)
        {
            try
            {
                if (userList == null)
                    return BadRequest();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState.GetModelErrors());

                if (userList.Any())
                {
                    foreach (User user in userList)
                    {
                        _userRepository.Delete(user);
                    }
                }
                await _userRepository.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }           
         
        }
        private async Task SaveUser(User user)
        {
            _userRepository.Add(user);
            await _userRepository.SaveChangesAsync();
        }

        private async Task UpdateUser(User user)
        {
            await SaveTagCollectionAsync(user.TagCollection, user.Id);
            await SaveEmailCollectionAsync(user.EmailCollection, user.Id);
            await SavePhoneCollectionAsync(user.PhoneCollection, user.Id);
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();
        }

        private async Task SaveTagCollectionAsync(ICollection<Tag> tagCollection, int uid)
        {
            List<Tag> dontDeleteList = new List<Tag>();
            List<Tag> allTags = _tagRepository.GetAll().Where(w => w.UserId == uid).ToList();
            foreach (Tag tag in tagCollection)
            {
                Tag oldTag = await _tagRepository.GetSingleAsync(tag.Id);
                if (oldTag == null)
                {
                    Tag newTag = tag;
                    newTag.UserId = uid;
                    _tagRepository.Add(newTag);
                    dontDeleteList.Add(newTag);
                }
                else
                    dontDeleteList.Add(oldTag);
            }
            _tagRepository.DeleteAllNotInSet(dontDeleteList, allTags);
            await _tagRepository.SaveChangesAsync();
        }

        private async Task SaveEmailCollectionAsync(ICollection<Email> emailCollection, int uid)
        {
            List<Email> dontDeleteList = new List<Email>();
            List<Email> allEmails = _emailRepository.GetAll().Where(w => w.UserId == uid).ToList();
            foreach (Email email in emailCollection)
            {
                Email oldEmail = await _emailRepository.GetSingleAsync(email.Id);
                if (oldEmail == null)
                {
                    Email newEmail = email;
                    newEmail.UserId = uid;
                    _emailRepository.Add(newEmail);
                    dontDeleteList.Add(newEmail);
                }
                else
                    dontDeleteList.Add(oldEmail);
            }
            _emailRepository.DeleteAllNotInSet(dontDeleteList, allEmails);
            await _emailRepository.SaveChangesAsync();
        }

        private async Task SavePhoneCollectionAsync(ICollection<Phone> phoneCollection, int uid)
        {
            List<Phone> dontDeleteList = new List<Phone>();
            List<Phone> allEmails = _phoneRepository.GetAll().Where(w => w.UserId == uid).ToList();
            foreach (Phone phone in phoneCollection)
            {
                Phone oldPhone = await _phoneRepository.GetSingleAsync(phone.Id);
                if (oldPhone == null)
                {
                    Phone newPhone = phone;
                    newPhone.UserId = uid;
                    _phoneRepository.Add(newPhone);
                    dontDeleteList.Add(newPhone);
                }
                else
                    dontDeleteList.Add(oldPhone);
            }
            _phoneRepository.DeleteAllNotInSet(dontDeleteList, allEmails);
            await _phoneRepository.SaveChangesAsync();
        }

    }
}
