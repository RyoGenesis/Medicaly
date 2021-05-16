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
                fileName = konsultasi.Nama + "_" + konsultasi.SpesialisId + "_" + fileName + extension;
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

        public static Konsultasi getKonsultasiById(int id)
        {
            if (id.ToString() != null)
            {
                return KonsultasiRepository.getKonsultasiById(id);
            }

            return null;
        }

        public static bool editKonsultasi(Konsultasi konsultasi)
        {
            if (KonsultasiRepository.getKonsultasiById(konsultasi.Id) != null)
            {
                Konsultasi oldKonsultasi = KonsultasiRepository.getKonsultasiById(konsultasi.Id);

                oldKonsultasi.Jawaban = konsultasi.Jawaban;
                oldKonsultasi.Status = 1;
                oldKonsultasi.DoktorId = konsultasi.DoktorId;

                return KonsultasiRepository.updateKonsultasi(oldKonsultasi);
            }

            return false;
        }
    }
}