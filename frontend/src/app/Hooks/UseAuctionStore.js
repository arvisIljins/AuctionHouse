import { createWithEqualityFn } from "zustand/traditional";

const initialState = {
  auctions: [],
  totalCount: 0,
  pageCount: 0,
};

export const useAuctionStore = createWithEqualityFn((set) => ({
  ...initialState,
  setData: (data) => {
    set(() => ({
      auctions: data.items,
      totalCount: data.totalCount,
      pageCount: data.pageCount,
    }));
  },
  setCurrentPrice: (auctionId, amount) =>
    set((state) => ({
      auctions: state.auctions.map((auction) =>
        auction.id == auctionId
          ? { ...auction, currentHightBid: amount }
          : auction
      ),
    })),
}));
