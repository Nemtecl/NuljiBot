using Discord;
using NuljiBot.Modules;

namespace NuljiBot.Services
{

    /// <summary>
    /// Classe de base héritée par tous les services.
    /// Ajoute des fonctionnalités et des propriétés aux services
    /// </summary>
    public class NuljiService
    {
        /// <summary>
        /// Référence au module parent.
        /// </summary>
        private NuljiModule _parentModule = null;

        /// <summary>
        /// Met à jour le module parent
        /// Doit toujours être appelé dans le constructeur du module.
        /// </summary>
        /// <param name="parentModule"></param>
        public void SetParentModule(NuljiModule parentModule)
        {
            _parentModule = parentModule;
        }

        /// <summary>
        /// Permet de répondre dans le channel courrant
        /// </summary>
        /// <param name="reply"></param>
        /// <param name="emb"></param>
        protected async void Reply(string reply, EmbedBuilder emb = null)
        {
            if (_parentModule == null) return;
            if (emb == null)
                await _parentModule.ServiceReplyAsync(reply);
            else
                await _parentModule.ServiceReplyAsync(reply, emb);
        }
    }
}
