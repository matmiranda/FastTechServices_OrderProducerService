namespace OrderProducerService.Application
{
    public static class QueueNames
    {
        //Pedido foi criado com sucesso
        public const string OrderPlaced = "order.placed";
        //Cliente ou sistema cancelou um pedido
        public const string OrderCancelled = "order.cancelled";
    }
}
