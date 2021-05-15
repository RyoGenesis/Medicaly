using Medicaly.Models;
using Medicaly.Repositories;
using Medicaly.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medicaly.Services
{
    public static class KonsultasiService
    {
        public static KonsultasiViewModel getKonsultasiView(int spesialisId)
        {
            List<Konsultasi> konsultasis = KonsultasiRepository.getKonsultasiBySpesialis(spesialisId) ;
            KonsultasiViewModel konsultasiView = new KonsultasiViewModel();

            konsultasiView.konsultasi = konsultasis;

            return konsultasiView;
        }

        public static bool editKonsultasi(int id)
        {
            if (KonsultasiRepository.getKonsultasiById(id) != null)
            {

                return KonsultasiRepository.updateKonsultasi(KonsultasiRepository.getKonsultasiById(id));
            }

            return false;
        }
    }
}