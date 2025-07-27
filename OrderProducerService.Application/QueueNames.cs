namespace OrderProducerService.Application
{
    public static class QueueNames
    {
        //Pedido foi criado com sucesso
        public const string OrderPlaced = "order.placed";
        //Cliente ou sistema cancelou um pedido
        public const string OrderCancelled = "order.cancelled";
        //Cozinha iniciou o preparo
        public const string OrderPreparationStarted = "order.preparation.started";
        //Pedido está pronto para retirada ou entrega
        public const string OrderReady = "order.ready";
        //Pedido finalizado e entregue
        public const string OrderDelivered = "order.delivered";
    }
}
