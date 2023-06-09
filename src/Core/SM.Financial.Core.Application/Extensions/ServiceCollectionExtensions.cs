﻿#nullable disable

using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SM.Financial.Core.Application.AutoMappings;
using SM.Financial.Core.Application.Commands.AccountReceivable;
using SM.Financial.Core.Application.Commands.BillToPay;
using SM.Financial.Core.Application.Consumers;
using SM.Financial.Core.Application.Handlers;
using SM.Financial.Core.Application.Models;
using SM.Financial.Core.Application.Queries.AccountReceivable;
using SM.Financial.Core.Application.Queries.BillToPay;
using SM.MQ.Configuration;
using SM.MQ.Extensions;
using SM.MQ.Models;
using SM.Resource.Communication.Mediator;
using SM.Resource.Util;
using System.Reflection;
using IPublisher = SM.MQ.Configuration.IPublisher;

namespace SM.Financial.Core.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            // AutoMapping
            services.AddAutoMapper(cfg => cfg.AddProfile(new MappingProfile()), typeof(object));

            // Mediator
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Query
            services.AddScoped<IRequestHandler<GetBillToPayByIdQuery, BillToPayModel>, BillToPayQueryHandler>();
            services.AddScoped<IRequestHandler<GetAllBillToPayQuery, IEnumerable<BillToPayModel>>, BillToPayQueryHandler>();

            services.AddScoped<IRequestHandler<GetAccountReceivableByIdQuery, AccountReceivableModel>, AccountReceivableQueryHandler>();
            services.AddScoped<IRequestHandler<GetAllAccountReceivableQuery, IEnumerable<AccountReceivableModel>>, AccountReceivableQueryHandler>();

            // Command
            services.AddScoped<IRequestHandler<AddBillToPayCommand, DefaultResult>, BillToPayCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateBillToPayCommand, DefaultResult>, BillToPayCommandHandler>();

            services.AddScoped<IRequestHandler<AddAccountReceivableCommand, DefaultResult>, AccountReceivableCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateAccountReceivableCommand, DefaultResult>, AccountReceivableCommandHandler>();

            // RabbitMQ
            services.AddRabbitMq(configuration);
        }

        public static void AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var builder = new BuilderBus(configuration["RabbitMq:ConnectionString"])
            {
                Consumers = new HashSet<Consumer>
                {
                    new Consumer(
                        queue: configuration["RabbitMq:ConsumerBillToPay"],
                        typeConsumer: typeof(RPCConsumerBillToPay),
                        quorumQueue: true
                    ),
                    new Consumer(
                        queue: configuration["RabbitMq:ConsumerAccountReceivable"],
                        typeConsumer: typeof(RPCConsumerAccountReceivable),
                        quorumQueue: true
                    )
                },

                Publishers = new HashSet<IPublisher>
                {
                    new Publisher<RequestIn>(queue: configuration["RabbitMq:ConsumerBillToPay"]),
                    new Publisher<RequestIn>(queue: configuration["RabbitMq:ConsumerAccountReceivable"]),
                },

                Retry = new Retry(retryCount: 3, interval: TimeSpan.FromSeconds(60))
            };
            services.AddEventBus(builder);
        }
    }
}
