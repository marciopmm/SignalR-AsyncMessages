using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading;

namespace AsyncMessages.Web
{
    public class MessageHub : Hub
    {
        private event Action<string> MessageCreated;
        private readonly string[] messages = new string[] {
            "A vida é dura pra quem é mole",
            "Mais vale um seio na mão do que dois no sutiã.",
            "Pau que nasce torto mija fora da bacia.",
            "Money que é good nóis num have.",
            "Minha vida é andar por esse país.",
            "O amor é cego, mas o casamento devolve a visão.",
            "A escola é uma instituição que vende diplomas, o aluno é o consumidor querendo comprar, e o professor é a pessoa que atrapalha as negociações.",
            "O psicólogo disse que tenho múltiplas personalidades, mas nós não concordamos com isso.",
            "Eu achava que era indeciso... mas agora não estou tão certo disto.",
            "É injusto. Quando a mulher fica grávida, todos correr a passar a mão pela barriga e a dizer \"parabéns\". Mas ninguém vai apalpar o seu saco e dizer \"bom trabalho\"",
            "Sempre a mesma coisa todo o santo dia... filho faz isso, filho faz aquilo... Não vejo a hora de fazer 40 anos e sair de casa.",
            "Fora Dilma e leve o PT junto."
        };

        public MessageHub()
        {
            MessageCreated += MessageHub_MessageCreated;
        }

        private void MessageHub_MessageCreated(string message)
        {
            Clients.All.broadcastMessage(message);
        }

        public void StartService()
        {
            Thread t = new Thread(new ThreadStart(RunService));
            t.Start();
        }

        public void RunService()
        {
            int index = 0, temp = 0;
            Random random = new Random();

            while (true)
            {
                do
                {
                    temp = random.Next(messages.Length);
                } while (temp == index);

                index = temp;

                if (MessageCreated != null)
                {
                    MessageCreated(messages[index]);
                }
                Thread.Sleep(4000);
            }
        }
    }
}