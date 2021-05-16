using Medicaly.Models;
using Medicaly.Repositories;
using Medicaly.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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

        public static bool addKonsultasi(Konsultasi konsultasi, string path)
        {
            if (!path.Equals(""))
            {
                string fileName = Path.GetFileNameWithoutExtension(konsultasi.ImageUpload.FileName);
                string extension = Path.GetExtension(konsultasi.ImageUpload.FileName);
                fileName = konsultasi.Nama + "_" + konsultasi.Spesiali.Nama + "_" + fileName + extension;
                konsultasi.FilePendukung = fileName;
                konsultasi.ImageUpload.SaveAs(Path.Combine(path, fileName));
            }

            // Default not answered
            konsultasi.Status = 0;

            if (KonsultasiRepository.addKonsultasi(konsultasi))
            {
                return true;
            }

            return false;
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