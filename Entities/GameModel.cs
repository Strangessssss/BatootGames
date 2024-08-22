using System.ComponentModel.DataAnnotations;
using BatootGames.Messages;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace BatootGames.Entities
{
    public partial class GameModel
    {
        public int GameId { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? Developer { get; set; }
        
        public ICollection<UserGame> UserGames { get; set; }
        public ICollection<Comment> Comments { get; set; }

        [RelayCommand]
        private void About()
        {
            WeakReferenceMessenger.Default.Send(new GameIdMessage() { Id = GameId }, "openAbout");
        }
        
        [RelayCommand]
        private void Add()
        {
            WeakReferenceMessenger.Default.Send(new GameIdMessage { Id = GameId }, "addGame");
        }

        private void Rate()
        {
            
        }
        
    }
}