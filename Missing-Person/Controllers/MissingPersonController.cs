using FaceRecognitionDotNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Missing_Person.Models;
using Missing_Person.Repository;
using Missing_Person.ViewModel;

namespace Missing_Person.Controllers
{
    [Authorize]
    public class MissingPersonController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IMissingPersonRepository imissingPersonRepository;
        private readonly UserManager<User> userManager;
        public MissingPersonController(IMissingPersonRepository imissingPersonRepository, UserManager<User> userManager, IWebHostEnvironment webHostEnvironment)
        {
            this.userManager = userManager;
            this.imissingPersonRepository = imissingPersonRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpGet]
        public IActionResult MyPost()
        {
            string userId = userManager.GetUserId(HttpContext.User);
            List<MissingPerson> missingPerson = imissingPersonRepository.GetMissingPersonByUserId(userId);
            var displayAllViewModel = new DisplayAllViewModel
            {
                MissingPeople = missingPerson
            };
            return View(displayAllViewModel);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            MissingPerson missingPerson = imissingPersonRepository.GetMissingPerson(id);
            if (missingPerson == null)
            {
                Response.StatusCode = 404;
                return View("NotFound");
            }
            var displayAllViewModel = new DisplayAllViewModel
            {
                MissingPerson = missingPerson
            };
            return View(displayAllViewModel);
        }
        [HttpPost]
        public IActionResult Edit(MissingPerson missingPerson)
        {
            
            //for the image
            string imgPath = @"MissingPerson\Image\" + Guid.NewGuid().ToString() + "_" + missingPerson.ImagePath.FileName;
            string serverPath = Path.Combine(webHostEnvironment.WebRootPath, imgPath);
            missingPerson.ImagePath.CopyTo(new FileStream(serverPath, FileMode.Create));
            missingPerson.ImageUrl = imgPath;

            MissingPerson updatedMissingPerson = imissingPersonRepository.UpdateMissingPerson(missingPerson);

            var displayAllViewModel = new DisplayAllViewModel
                {
                    MissingPerson = updatedMissingPerson
                };
                return RedirectToAction("Details", new { id = displayAllViewModel.MissingPerson.Id });
        }
        [HttpPost]
        public IActionResult Register(MissingPerson missingPerson)
        {
            if (ModelState.IsValid)
            {
                string imgPath = @"MissingPerson\Image\" + Guid.NewGuid().ToString() + "_" + missingPerson.ImagePath.FileName;
                string serverPath = Path.Combine(webHostEnvironment.WebRootPath, imgPath);
                missingPerson.ImagePath.CopyTo(new FileStream(serverPath, FileMode.Create));
                missingPerson.ImageUrl = imgPath;
                //get the user id who registered the missing person
                missingPerson.User_Id = userManager.GetUserId(HttpContext.User);
                missingPerson.Status = "Not Found";
                MissingPerson newMissingPerson = imissingPersonRepository.AddMissingPerson(missingPerson);
                return RedirectToAction("Details", new { id = newMissingPerson.Id });
            }
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ViewResult DisplayAll()
        {
            var model = imissingPersonRepository.GetMissingPeople();
            var displayAllViewModel = new DisplayAllViewModel
            {
                MissingPeople = model
            };
            return View(displayAllViewModel);
        }
        [AllowAnonymous]
        [HttpGet]
        public ViewResult Details(int id)
        {
            MissingPerson missingPerson = imissingPersonRepository.GetMissingPerson(id);
            if (missingPerson == null)
            {
                Response.StatusCode = 404;
                return View("NotFound");
            }
            var displayAllViewModel = new DisplayAllViewModel()
            {
                MissingPerson = missingPerson,
            };
            return View(displayAllViewModel);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Search(MissingPerson? mp, string? name)
        {
            if (mp.ImagePath != null)
            {
                string imgPaths = "";
                List<string> result = new List<string>();
                string path = "C:\\Users\\Yeabsira\\Documents\\GitHub\\Missing-Person-Registration-and-Searching\\Missing-Person\\wwwroot\\";

                //save the image provided by the user in the folder Search
                string imgPath = @"MissingPerson\Search\" + Guid.NewGuid().ToString() + "_" + mp.ImagePath.FileName;
                string serverPath = Path.Combine(webHostEnvironment.WebRootPath, imgPath);
                mp.ImagePath.CopyTo(new FileStream(serverPath, FileMode.Create));
                imgPaths= imgPath;

                //get all the files in the folder Image
                var imagesPath = Path.Combine(webHostEnvironment.WebRootPath, "MissingPerson", "Image");
                //get all the files in the folder with the extension .jpg
                var imageFiles = Directory.GetFiles(imagesPath, "*.jpg", SearchOption.AllDirectories);
                //remove the webroot path from the image path and replace the \ with / (from relative to absolute file path)
                var imagePaths = imageFiles.Select(file => Path.Combine("/", file.Replace(webHostEnvironment.WebRootPath, "")));
                
                try
                {
                    //create a face recognition object and load the model from the folder models1 
                    string currentDirectory = "models1";
                    FaceRecognition fr = FaceRecognition.Create(currentDirectory);

                    //concatenate the path of the image to the path of the folder
                    string img = path + imgPaths;

                    var FirstImageLoaded = FaceRecognition.LoadImageFile(img);
                    var FirstImageEncoded = fr.FaceEncodings(FirstImageLoaded).First();

                    List<string> imagesToCompare = new List<string>();
                    foreach (var imageFile in imageFiles)
                    {
                        imagesToCompare.Add(imageFile);
                    }

                    foreach (var imagePath in imagesToCompare)
                    {
                        var ImageFromDbLoaded = FaceRecognition.LoadImageFile(imagePath);
                        var ImageFromDbEncoded = fr.FaceEncodings(ImageFromDbLoaded).FirstOrDefault();

                        if (ImageFromDbEncoded != null)
                        {
                            bool check = FaceRecognition.CompareFace(FirstImageEncoded, ImageFromDbEncoded);
                            if (check != false)
                            {
                                result.Add(Path.Combine("/", imagePath.Replace(webHostEnvironment.WebRootPath, "")));
                            }
                        }
                        else
                        {
                            //means the image is not encoded properly
                            result.Add(imagePath + "Not the right type of image");
                        }

                    }
                    List<string> Imgurls = new List<string>();
                    foreach (var x in result)
                    {
                        //removing unessary characters from the path
                        Imgurls.Add(x.Substring(1));
                    }
                    List<MissingPerson> missingPerson = imissingPersonRepository.GetMissingPersonByImage(Imgurls);
                    DisplayAllViewModel displayAllViewModel = new DisplayAllViewModel()
                    {
                        MissingPeople = missingPerson,
                    };
                    return View(displayAllViewModel);

                }
                catch (Exception e123)
                {
                    Console.WriteLine(e123);
                }

            }
            else if(name != null)
            {
                List<MissingPerson> missingPerson = imissingPersonRepository.SearchByName(name);
                if (missingPerson != null)
                {
                    DisplayAllViewModel displayAllViewModel = new DisplayAllViewModel()
                    {
                        MissingPeople = missingPerson,
                    };
                    return View(displayAllViewModel);
                }
                else
                {
                    return View("NotFound");
                }
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
