using ConsoleApp1;
using Microsoft.AspNetCore.Mvc;
using System.Timers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.Controllers
{
    //[Route("api/[controller]")]
    [Route("api")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public static string filePath = "";
        public static AudioMetadata amd = new AudioMetadata();
        public delegate Task GetFileProps();

        public GetFileProps fileProps;



        
        // GET: api/<ValuesController>
        [HttpGet]
        [Route("2val")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        /*
         * 
        */
        [HttpPost]
        [Route("filename")]
        //[RequestSizeLimit(500000000)]
        public async Task<AudioMetadata> GetFileName(IFormFile file)
        {
            
            try {
                    
                filePath = Path.Combine(@"C:\Users\username\Music\fyveAudio\", file.FileName);                  
                Utility utility = new Utility(file, filePath);
                ;

                //Action<int> a = new Action<int>(utility.GetMetadata);
                // delegate FileMetaData;
                //fileProps = new GetFileProps(utility.writeFile);
                //Task t2 = Task.Run(utility.GetFileProperties);
                // Func<Task> action =  async () =>   await utility.GetMetadata();
                //fileProps = utility.GetFileProperties();

                //Task t1 = new Task(async () => { await utility.writeFile(); });

               
                    Task t1 = Task.Run(
                        async () => { await utility.writeFile(); }
                    );

                    Task t2 = t1.ContinueWith(async (result) => { await utility.GetMetadata(); });
                    Task.WaitAny(t2);
                    await Task.Delay(200);
                
                

               // Task t2 = new Task(async () => { await utility.GetMetadata(); });

                // t1.Start();
                // t2.Start();
                // t1.Start();
                /*
              Task.Run(() => {
                 while (true)
                 {
                     utility.semaphoreSlim.Wait();
                     if (utility.isComplete)
                     {
                         break;
                     }
                     utility.semaphoreSlim.Release();
                     Thread.Sleep(1000);
                 }
             });
             */
                //await t2.WaitAsync(2);
                return utility.amd;
                
               
            
            }catch(Exception ex) {


                return new AudioMetadata() { MediaType = "Exception"};//"Oops an exception has occured, here are some details:\n\n\n" + ex;
            }
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
