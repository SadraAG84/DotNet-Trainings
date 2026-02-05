using IdentityApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace IdentityApp.TagHelpers
{
    [HtmlTargetElement("td", Attributes = "asp-role-user")]
    public class RoleUsersTagHelper : TagHelper
    {
        // Dependencies
        // We need RoleManager to get role details and UserManager to check which users are in the role
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleUsersTagHelper(
            RoleManager<AppRole> roleManager,
            UserManager<AppUser> userManager
        )
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HtmlAttributeName("asp-role-user")]
        public string RoleId { get; set; } = null!;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            // Get users in the specified role
            var usersInRole = new List<string>();
            var role = await _roleManager.FindByIdAsync(RoleId);

            if (role != null)
            {
                // Iterate through all users to find those in the role
                var users = _userManager.Users;
                foreach (var user in users)
                {
                    if (await _userManager.IsInRoleAsync(user, role.Name!))
                    {
                        usersInRole.Add(user.UserName ?? "");
                    }
                }
                output.Content.SetHtmlContent(
                    usersInRole.Count == 0 ? "No users" : setHtml(usersInRole)
                );
            }
        }

        private string setHtml(List<string> usersInRole)
        {
            var html = "<ul>";
            foreach (var item in usersInRole)
            {
                html += $"<li>{item}</li>";
            }
            html += "</ul>";
            return html;
        }
    }
}
