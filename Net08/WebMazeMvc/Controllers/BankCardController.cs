using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Novacode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMazeMvc.EfStuff;
using WebMazeMvc.EfStuff.Model;
using WebMazeMvc.EfStuff.Repositories;
using WebMazeMvc.Models;
using WebMazeMvc.Services;

namespace WebMazeMvc.Controllers
{
    public class BankCardController : Controller
    {
        private readonly BankCardRepository _bankCardRepository;
        private readonly IMapper _mapper;
        private UserRepository _userRepository;
        private FileService _fileService;
        
        public BankCardController(BankCardRepository bankCardRepository, 
            IMapper mapper, 
            UserRepository userRepository, 
            FileService fileService)
        {
            _bankCardRepository = bankCardRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _fileService = fileService;
        }
        
        [HttpGet]
        public IActionResult BankCardGet(long id)
        {
            var card = _bankCardRepository.Get(id);
                      
            if (card == null)
            {
                throw new ArgumentNullException(nameof(card), $"Карты с id={id} нет в базе данных");
            }

            var viewModel = _mapper.Map<BankCardGetViewModel>(card);

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult BankCardAll()
        {
            var allCards = _bankCardRepository.GetAll();
            var viewModels = _mapper.Map<List<BankCardGetViewModel>>(allCards);

            return View(viewModels);
        }

        [HttpGet]
        public IActionResult BankCardAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BankCardAdd(BankCardAddViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var newCard = _mapper.Map<BankCard>(viewModel);
            newCard.Owner = _userRepository.Get(viewModel.OwnerId);
            _bankCardRepository.Save(newCard);

            return RedirectToAction("BankCardAll");
        }

        [HttpGet]
        public IActionResult BankCardRemove()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BankCardRemove(long id)
        {
            var card = _bankCardRepository.Get(id);

            if (card == null)
            {
                throw new ArgumentNullException(nameof(card), $"Карты с id={id} нет в базе данных");
            }

            _bankCardRepository.Remove(card);

            return RedirectToAction("BankCardAll");
        }

        [HttpGet]
        public IActionResult BankCardDelete(long id)
        {
            var card = _bankCardRepository.Get(id);                       
            _bankCardRepository.Remove(card);

            return RedirectToAction("BankCardAll");
        }
        
        [HttpGet]
        public IActionResult DownloadBankCard()
        {
            var pathToFile = _fileService.GetTempDocxFilePath();
            var allCards = _bankCardRepository.GetAll();
            
            using (var file = DocX.Create(pathToFile))
            {
                Table table = file.AddTable(allCards.Count + 1, 4);
                table.Alignment = Alignment.center;
                table.Design = TableDesign.TableGrid;
                table.Rows[0].Cells[0].Paragraphs.First().Append("Id card");
                table.Rows[0].Cells[1].Paragraphs.First().Append("Card Number");
                table.Rows[0].Cells[2].Paragraphs.First().Append("Validity Month");
                table.Rows[0].Cells[3].Paragraphs.First().Append("Validity Year");

                for (int i = 1; i <= allCards.Count; i++)
                {           
                    table.Rows[i].Cells[0].Paragraphs.First().Append(allCards[i - 1].Id.ToString());
                    table.Rows[i].Cells[1].Paragraphs.First().Append(allCards[i - 1].CardNumber);
                    table.Rows[i].Cells[2].Paragraphs.First().Append(allCards[i - 1].ValidityMonth.ToString());
                    table.Rows[i].Cells[3].Paragraphs.First().Append(allCards[i - 1].ValidityYear.ToString());
                }

                file.InsertTable(table);
                file.Save();
            }

            return PhysicalFile(pathToFile,
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                "FileForUserName.docx");
        }
    }
}