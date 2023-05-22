using FaceRecognitionDotNet;
using Microsoft.AspNetCore.Mvc;
using Missing_Person.Models;

namespace Missing_Person.Controllers
{
    public class MissingPersonController : Controller
    {
        private readonly ILogger<MissingPersonController> _logger;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MissingPersonController(ILogger<MissingPersonController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(MissingPerson mp)
        {
            if (mp.ImagePath != null)
            {
                List<string> imgPaths = new List<string>();
                List<string> result = new List<string>();
                string imgPath = "";
                string path = "C:\\Users\\Yeabsira\\Documents\\GitHub\\Missing-Person-Registration-and-Searching\\Missing-Person\\wwwroot";
                string path2 = "C:\\Users\\Yeabsira\\Documents\\GitHub\\Missing-Person-Registration-and-Searching\\Missing-Person\\wwwroot\\";
                foreach (var file in mp.ImagePath)
                {
                    imgPath = @"MissingPerson\Search\" + Guid.NewGuid().ToString() + "_" + file.FileName;
                    string serverPath = Path.Combine(webHostEnvironment.WebRootPath, imgPath);
                    file.CopyTo(new FileStream(serverPath, FileMode.Create));
                    imgPaths.Add(path + imgPath);
                }

                var imagesPath = Path.Combine(webHostEnvironment.WebRootPath, "MissingPerson", "Image");
                var imageFiles = Directory.GetFiles(imagesPath, "*.jpg", SearchOption.AllDirectories);

                var imagePaths = imageFiles.Select(file => Path.Combine("/", file.Replace(webHostEnvironment.WebRootPath, "")));//.Replace("\\", "\\"))).ToList();

                try
                {
                    string currentDirectory = "C:\\Users\\Yeabsira\\Documents\\GitHub\\Missing-Person-Registration-and-Searching\\Missing-Person\\models1";
                    FaceRecognition fr = FaceRecognition.Create(currentDirectory);

                    string img = path2 + imgPath;
                    var dlibToComBuf = FaceRecognition.LoadImageFile(img);
                    var enToCompare = fr.FaceEncodings(dlibToComBuf).First();

                    List<string> imagesToCompare = new List<string>();
                    foreach (var imageFile in imageFiles)
                    {
                        imagesToCompare.Add(imageFile);
                    }

                    foreach (var imagePath in imagesToCompare)
                    {
                        var dlibToComBuf2 = FaceRecognition.LoadImageFile(imagePath);
                        var enToCompare2 = fr.FaceEncodings(dlibToComBuf2).FirstOrDefault();

                        if (enToCompare2 != null)
                        {
                            bool check = FaceRecognition.CompareFace(enToCompare, enToCompare2);
                            if (check != false)
                            {
                                result.Add("True");
                            }
                        }
                        else
                        {
                            //means the image is not encoded properly
                            result.Add("Not the right type of image");
                        }

                    }

                    foreach (var x in result)
                    {
                        Console.WriteLine(x);
                    }

                }
                catch (Exception e123)
                {
                    Console.WriteLine(e123);
                }

            }
            return RedirectToAction("Index", "Home");
        }

    }
}
