using System;
using Photon.Pun;

namespace Business.Extensions
{
    public static class PhotonExtensions
    {
        public static void SyncWithRPC(this PhotonView view, Action method, RpcTarget target, params object[] parameters)
        {
            view.RPC(method.Method.Name, target, parameters);
        }
    }
}