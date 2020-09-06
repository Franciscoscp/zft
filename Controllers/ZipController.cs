using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using zip;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace zft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZipController : ControllerBase
    {
       static LinkedList<Zip> zipList = new LinkedList<Zip>();

        // GET: api/<ZipController>
        [HttpGet]
        public IEnumerable<Zip> Get()
        {
            return zipList.ToArray();
        }

        // GET api/<ZipController>/5
        [HttpGet("{id}")]
        public Zip Get(int id)
        {
            return zipList.ElementAt(id);
        }

        // POST api/<ZipController>
        [HttpPost]
        public void Post([FromBody] Zip z)
        {
            string tempFileName = Path.GetTempFileName();
            using (System.Net.WebClient client =  new System.Net.WebClient()){
               client.DownloadFile(z.zipUri.ToString(),tempFileName);
            }

            System.Diagnostics.Debug.Write("" + z);

            FileStream file = new FileStream(tempFileName, FileMode.Open);
            ZipArchive zippedData = new ZipArchive(file, ZipArchiveMode.Read);
            ReadOnlyCollection<ZipArchiveEntry> compressedFiles =  zippedData.Entries;

            LinkedList<zip.ZipInfo> infos = new LinkedList<zip.ZipInfo>();

            foreach (ZipArchiveEntry compressed in compressedFiles)
            {
                zip.ZipInfo info = new zip.ZipInfo();
                info.compressedSize= compressed.CompressedLength;
                info.fullSize = compressed.Length;
                info.name = compressed.FullName;
                infos.AddLast(info);
            }

            z.data = infos;
     
            zipList.AddLast(z);
            file.Close();
            System.IO.File.Delete(tempFileName);
        }

        // PUT api/<ZipController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ZipController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
