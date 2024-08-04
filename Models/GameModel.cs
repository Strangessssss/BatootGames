using System.ComponentModel.DataAnnotations;
using BatootGames.Messages;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace BatootGames.Models
{
    public partial class GameModel
    {
        [Key] public int Id { get; set; }
        [MaxLength(450)] public string? Name { get; set; }
        [MaxLength(1000)] public string? Image { get; set; }
        [MaxLength(1000)] public string? Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public float Rating { get; set; }
        public bool Saved { get; set; }
        [MaxLength(450)] public string? Developer { get; set; }

        [RelayCommand]
        private void About()
        {
            WeakReferenceMessenger.Default.Send(new GameIdMessage() { Id = Id });
        }
        
        [RelayCommand]
        private void Add()
        {
            if (Saved)
                Rating = 0;
            Saved = !Saved;
            WeakReferenceMessenger.Default.Send(new SaveDeleted());
        }
        
        private void Rate()
        {
            Saved = true;
            WeakReferenceMessenger.Default.Send(new RateMessage()
            {
                GameId = Id,
                Rating = Rating
            });
        }
        
        [RelayCommand]
        private void Plus()
        {
            if (Rating >= 5)
                return;
            Rating = Rating + 0.5f;
            Rate();
        }
        
        [RelayCommand]
        private void Minus()
        {
            if (Rating == 0)
                return;
            Rating = Rating - 0.5f;
            Rate();
        }
    }
}