using Accapt.Core.DTOs;
using Accapt.Core.Servies.InterFace;
using Accapt.DataLayer.Context;
using Accapt.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.Core.Servies
{
    public class UpdateClientService : IUpdateClientService
    {
        private readonly AccaptFContext _context;

        public UpdateClientService(AccaptFContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        public async Task<bool> AddNewUpdate(AddUpdateClientDTO addUpdateClientDTO)
        {
            var client = await _context.ClientUpdates.FirstOrDefaultAsync(c => c.Id == addUpdateClientDTO.Id);

            if (client == null)
                return false;

            client.DownloadUrl = addUpdateClientDTO.Downnload;
            client.Version = addUpdateClientDTO.Version;
            client.IsMandentory = addUpdateClientDTO.IsMandetory;
            client.RealeseNote = addUpdateClientDTO.Note;

            _context.ClientUpdates.Update(client);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ClientUpdate> CehckUpdate(string version)
        {
            var client = await _context.ClientUpdates.AnyAsync(c => c.Version == version);

            if (client)
                return null;

            return await _context.ClientUpdates.FirstOrDefaultAsync(c => c.Id == 1);
        }
    }
}
