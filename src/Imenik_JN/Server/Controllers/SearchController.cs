using Imenik_JN.Server.Data.Interfaces;
using Imenik_JN.Server.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Imenik_JN.Server.Controllers
{
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly IUserRepository _userRepository; 

        public SearchController(IUserRepository userRepository, ITagRepository tagRepository, IPhoneRepository phoneRepository, IEmailRepository emailRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("{term}")]
        public IActionResult Search(string term)
        {
            try
            {

                string regex = @"^[\w\@\#\$\&\*\(\)\-\+\]\[\ \'\;\:\?\.\,\!]+$"; // matches all characters including čćšđž and all numbers, also matches blank spaces, and all punctuation marks inside the regex
         
                if (string.IsNullOrEmpty(term)
                    || string.IsNullOrWhiteSpace(term)
                    || !System.Text.RegularExpressions.Regex.IsMatch(term, regex))
                    return BadRequest();

                string trimTerm = term.Trim();

                IQueryable<User> searchResult = _userRepository.AllIncluding(inc => inc.TagCollection, inc => inc.PhoneCollection, inc => inc.EmailCollection)
                    .Where(w =>
                    w.Address.Contains(trimTerm)
                    || w.FirstName.Contains(trimTerm)
                    || w.LastName.Contains(trimTerm)
                    || w.TagCollection.Select(s => s.TagName).Any(a => a.Contains(trimTerm))); 

                return new OkObjectResult(searchResult);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
