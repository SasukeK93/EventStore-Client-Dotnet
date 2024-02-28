namespace EventStore.Client.PersistentSubscriptions.Tests.SubscriptionToAll;

public class connect_to_existing_without_permissions : IClassFixture<connect_to_existing_without_permissions.Fixture> {
	private readonly Fixture _fixture;
	public connect_to_existing_without_permissions(Fixture fixture) => _fixture = fixture;

	[SupportsPSToAll.Fact]
	public Task throws_access_denied() =>
		Assert.ThrowsAsync<AccessDeniedException>(
			async () => {
				await using var subscription = _fixture.Client.SubscribeToAll("agroupname55");
				await subscription.AnyAsync().AsTask().WithTimeout();
			});

	public class Fixture : EventStoreClientFixture {
		public Fixture() : base(noDefaultCredentials: true) { }

		protected override Task Given() =>
			Client.CreateToAllAsync("agroupname55", new(), userCredentials: TestCredentials.Root);

		protected override Task When() => Task.CompletedTask;
	}
}
