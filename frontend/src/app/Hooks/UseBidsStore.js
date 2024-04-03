import { find } from "lodash";
import { create } from "zustand";

const initialState = {
  bids: [],
};

export const useBidsStore = create((set) => ({
  ...initialState,
  setBids: (bids) => {
    set(() => ({
      bids,
    }));
  },
  addBid: (data) => {
    set((state) => ({
      bids: find(!state.bids, (x) => x.id === bid.id)
        ? [bid, ...state.bids]
        : state.bids,
    }));
  },
}));
