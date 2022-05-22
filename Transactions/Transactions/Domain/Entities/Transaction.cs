﻿using Transactions.Domain.Common;
using Transactions.Domain.Enums;
using Transactions.Domain.Events;

namespace Transactions.Domain.Entities;

public class Transaction : IHasDomainEvent
{
    private Transaction()
    {

    }

    public Transaction(string? id, DateTime date, TransactionStatus status, string? from, string? reference, string currency, decimal amount)
    {
        if(amount == 0) 
        {
            throw new ArgumentException("Amount must be greater than 0.");
        }

        Id = id ?? Guid.NewGuid().ToUrlFriendlyString();
        Date = date;
        Status = status;
        From = from;
        Reference = reference;
        Currency = currency;
        Amount = amount;

        DomainEvents.Add(new TransactionRegistered(Id));
    }

    public string Id { get; set; } = null!;

    public DateTime Date { get; set; }

    public TransactionStatus Status { get; private set; } = TransactionStatus.Unverified;

    public string? From { get; set; }

    public string? Reference { get; set; }

    public string Currency { get; set; } = null!;

    public decimal Amount { get; set; }

    public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();

    public void SetStatus(TransactionStatus status)
    {
        Status = status;

        DomainEvents.Add(new TransactionStatusChanged(Id, status));
    }
}