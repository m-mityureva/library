using Library.Controllers;
using Library.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Library.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "user-identity")]
    public class MenuTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;
        private IMenuManager menuManager;
        private IUrlHelper urlHelper;

        public MenuTagHelper(IUrlHelperFactory helperFactory, IMenuManager menuManagerService)
        {
            urlHelperFactory = helperFactory;
            menuManager = menuManagerService;
        }

        public IIdentity UserIdentity { get; set; }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder result = new TagBuilder("div");

            result.InnerHtml.AppendHtml(GetDropDownMenu());
            result.InnerHtml.AppendHtml(GetLoginMenu());

            output.Content.AppendHtml(result.InnerHtml);
        }

        private TagBuilder GetDropDownMenu()
        {
            TagBuilder dropdown = new TagBuilder("div");
            dropdown.AddCssClass("dropdown mr-2 col");

            if (UserIdentity.IsAuthenticated)
            {
                var menuItems = menuManager.GetMenuItems(UserIdentity.Name);

                TagBuilder dropdownToggle = new TagBuilder("a");
                dropdownToggle.AddCssClass("text-white dropdown-toggle");
                dropdownToggle.Attributes.Add("data-toggle", "dropdown");
                dropdownToggle.InnerHtml.Append(UserIdentity.Name);

                TagBuilder dropdownMenu = new TagBuilder("div");
                dropdownMenu.AddCssClass("dropdown-menu dropdown-menu-right");

                foreach (var menuItem in menuItems)
                {
                    TagBuilder item = new TagBuilder("a");
                    item.AddCssClass("dropdown-item");
                    item.InnerHtml.Append(menuItem.Name);
                    item.Attributes["href"] = urlHelper.Action(menuItem.Action, menuItem.Controller);
                    dropdownMenu.InnerHtml.AppendHtml(item);
                }

                dropdown.InnerHtml.AppendHtml(dropdownToggle);
                dropdown.InnerHtml.AppendHtml(dropdownMenu);

                TagBuilder userDiv = new TagBuilder("div");
                userDiv.AddCssClass("text-right");
            }
            return dropdown;
        }

        private TagBuilder GetLoginMenu()
        {
            TagBuilder loginMenu = new TagBuilder("div");
            loginMenu.AddCssClass("text-right");

            if (UserIdentity.IsAuthenticated)
            {
                TagBuilder logout = new TagBuilder("a");
                logout.AddCssClass("text-white");
                logout.InnerHtml.Append("Log Out");
                logout.Attributes["href"] = urlHelper.Action(nameof(AccountController.Logout), "Account");
                loginMenu.InnerHtml.AppendHtml(logout);
            }
            else
            {
                TagBuilder login = new TagBuilder("a");
                login.AddCssClass("text-white");
                login.InnerHtml.Append("Log In");
                login.Attributes["href"] = urlHelper.Action(nameof(AccountController.Login), "Account");

                TagBuilder register = new TagBuilder("a");
                register.AddCssClass("text-white");
                register.InnerHtml.Append("Sign Up");
                register.Attributes["href"] = urlHelper.Action(nameof(AccountController.Register), "Account");
                
                loginMenu.InnerHtml.AppendHtml(login);
                loginMenu.InnerHtml.AppendHtml(" | ");
                loginMenu.InnerHtml.AppendHtml(register);
            }
            return loginMenu;
        }
    }
}
