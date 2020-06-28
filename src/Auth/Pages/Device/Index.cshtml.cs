﻿using System.Threading.Tasks;
using Auth.Models;
using IdentityServer4.Configuration;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Auth.Pages.Device
{
    public class IndexModel : DeviceAuthorizationPageModel
    {
        private readonly IOptions<IdentityServerOptions> _options;

        public IndexModel(
            IDeviceFlowInteractionService interaction, IOptions<IdentityServerOptions> options)
            : base(interaction)
        {
            _options = options;
        }

        [BindProperty]
        public DeviceAuthorizationViewModel Model { get; set; } = new DeviceAuthorizationViewModel();

        public async Task<IActionResult> OnGetAsync()
        {
            var userCodeParamName = _options.Value.UserInteraction.DeviceVerificationUserCodeParameter;
            var userCode = Request.Query[userCodeParamName];

            if (!string.IsNullOrWhiteSpace(userCode))
            {
                Model = await BuildViewModelAsync(userCode);

                if (Model == null)
                {
                    return RedirectToPage("/Error");
                }

                Model.ConfirmUserCode = true;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string userCode)
        {
            Model = await BuildViewModelAsync(userCode);

            if (Model == null)
            {
                return RedirectToPage("/Error");
            }

            return Page();
        }
    }
}
