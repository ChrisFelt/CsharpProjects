using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Chatter.ViewModels
{
    public partial class ChatViewModel : ObservableObject
    {
        // supports MainPage chat functionality
        // probable methods include:
        // LoadMessages (loads history?)
        // SendMessage
        [ObservableProperty]
        private ObservableCollection<object> _messageCollection = [];

        // add a new message to MessageCollection
        private void AddMessageToCollection(Message newMessage)
        {
            MessageCollection.Add(newMessage);
        }
    }
}
