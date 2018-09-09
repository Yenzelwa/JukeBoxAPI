namespace JukeBoxApi.Models
{
    public enum ResponseType
    {
        Success = 1,
        Failed = -1
    }

    public enum Platform
    {
        Website = 1,
        Mobisite,
        FeaturePhone,
        Ussd,
        Android,
        OldMobisite
    }

    public enum VoucherType
    {
        HollyTopUp = 1,
        Sni,
        Ott,
        BlueLabel
    }

    public enum VoucherTransactionType
    {
        VoucherRedeem = 1,
        Bonus
    }

    public enum Regions
    {
        Local = 1,
        Int = 2,
        AfricaBet = 3
    }

    public enum SMSTypes
    {
        Registration = 1,
        PasswordReset = 2,
        Fica = 3,
        FicaDocuments = 4,
        BookABet = 5,
        Withdrawal = 6,
        ReferAFriend = 7
    }

    public enum ClientTransactionTypes
    {
        Stake = 1,
        CashWithdrawal = 2,
        EFTDeposit,
        CreditCardDeposit,
        EFTWithdrawal = 5,
        RequestACB,
        MobilePayment,
        CashDeposit = 8,
        Winnings,
        Stakereturn,
        Tax,
        Journal = 12,
        Cancelled,
        Refunded,
        TransferPaymentApproved,
        Unrefunded,
        FundsTransfer,
        Payout,
        VoucherIssued,
        VoucherCancelled,
        Promotions = 21,
        DepositNotification,
        ReversalOfWithdrawal,
        BSCJournals,
        Debit,
        CreditCardDepositJournal,
        ReferAFriendNotification,
        TaxPayout,
        ReferAFriendDeposit,
        Win,
        LegTax,
        OnlineWithdrawal,
        DailyStakeCfwd,
        OnlineDeposit,
        DailyUnpaidTickets,
        BetTypeBonus,
        LoseStake,
        ClientPayout,
        VoucherRedeemed = 48,
        ObjectionStakeRefund,
        BankDeposit,
        EventsJournals,
        FundsTransferIn,
        FundsTransferOut,
        LoyaltyRedeemed = 60
    }

    public enum ApiBetCaptureGridRuleSetType
    {
        Lotto = 1,
        Open = 2,
        Exotic = 3
    }

    public enum ClientStatuses
    {
        Active = 1,
        Pending = 2,
        Closed = 3,
        LockedOut = 4,
        SelfExcluded = 9,
        NeverOpenAgain = 10
    }

    public enum BetTypeGroups
    {
        Straight = 1,
        Open,
        Exotic,
        LuckyNumbers,
        MatchSports,
        SpecialExotics
    }

    public enum EventStatuses
    {
        Active = 1,
        Suspended,
        Closed,
        Postponed,
        Abandoned,
        Final,
        PendingApproval,
        PendingResult,
        Disabled,
        Resulting
    }

    public enum BetStatuses
    {
        Active = 1,
        PendingApproval,
        Scratched,
        PaidOut,
        Losing,
        PendingRefund,
        Cancelled,
        Winning,
        RefundPaid,
        Abandoned
    }

    public enum PayoutTypes
    {
        FixedOdds = 1,
        StartingPrice,
        Open
    }

    public enum IdentityType
    {
        SAIdentityDocument = 2,
        SAPassport = 3
    }

    public enum EmailType
    {
        BetConfirmation,
        EmailChangedConfirmation,
        WithdrawalNotification,
        DepositNotification,
        PasswordResetNotification,
        BookABetConfirmation
    }
}
